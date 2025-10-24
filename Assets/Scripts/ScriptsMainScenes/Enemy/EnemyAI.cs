using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyEntity))]

public class EnemyAI : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private List<AlliesEntity> _enemys;
    [SerializeField] private State _startingState;

    [SerializeField] private float _isFightModeDistance = 5f;

    [SerializeField] private bool _isChasingEnemy = true;
    [SerializeField] private float _isChasingDistance = 5f;

    [SerializeField] private bool _isAttackingEnemy = true;
    [SerializeField] private float _isAttackingDistane = 2f;

    private DirectionalRotator _rotator;

    private NavMeshAgent _navMeshAgent;
    private EnemyEntity _enemyEntity;

    private State _currentState;
    private bool _isEnemyFightMode = false;
    private GameObject _activeWeapon;
    private Transform _targetEnemy;
    private AlliesEntity _closestEnemy;
    private bool _isClosestEnemyDead;
    private float _distanceToEnemy;
    private float _nextAttackTime;

    public bool isEnemyRunning
    {
        get
        {
            if (_navMeshAgent.velocity == Vector3.zero)
                return false;
            else
                return true;
        }
    }
    // ----------------------------------

    // Список сотояний врага
    private enum State
    {
        Idle,
        Roaming,
        ChasingToPlayer,
        ChasingToEnemy,
        WalkToPortal,
        FightMode,
        Attacking,
        Death
    } // ----------------------------------

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyEntity = GetComponent<EnemyEntity>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _rotator = new DirectionalRotator(null, transform);
    }

    private void Start()
    {
        _currentState = _startingState;
        _activeWeapon = transform.GetChild(1).gameObject;
        _targetEnemy = PortalPlayer.Instance.transform;
    }

    private void Update()
    {
        if (_enemyEntity.IsEnemyDead() == false)
        {
            if (FindClosestEnemy())
            {
                _targetEnemy = _closestEnemy.transform;
                CheckCurrentState();
                StateHandler();
            }
            else
            {
                CheckCurrentState();
                StateHandler();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out AlliesEntity Enemy))
        {
            _enemys.Add(Enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out AlliesEntity Enemy))
        {
            _enemys.Remove(Enemy);
        }
    }

    // Поле приватных методов
    private AlliesEntity FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (AlliesEntity enemy in _enemys)
        {
            if (enemy != null)
            {
                Vector3 diff = enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (distance > curDistance)
                {
                    _closestEnemy = enemy;
                    distance = curDistance;
                }
            }
        }
        return _closestEnemy;
    }

    private void StateHandler()
    {
        switch (_currentState)
        {
            case State.ChasingToPlayer:
                ChasingToPlayerTarget();
                CheckCurrentState();
                break;
            case State.ChasingToEnemy:
                ChasingToEnemyTarget();
                CheckCurrentState();
                break;
            case State.WalkToPortal:
                WalkToPortalTarget();
                CheckCurrentState();
                break;
            case State.FightMode:
                FightModeOn();
                CheckCurrentState();
                break;
            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                break;
        }
    }

    private void CheckCurrentState()
    {
        if (FindClosestEnemy())
        {
            _distanceToEnemy = Vector3.Distance(transform.position, _closestEnemy.transform.position);
            _isClosestEnemyDead = _closestEnemy.IsAlliesDead();
        }

        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        float distanceToPortal = Vector3.Distance(transform.position, PortalPlayer.Instance.transform.position);
        State newState = State.WalkToPortal;

        if (_isEnemyFightMode == false)
            if (distanceToPlayer <= _isFightModeDistance || distanceToPortal <= _isFightModeDistance)
                newState = State.FightMode;

        if (_isEnemyFightMode == false && FindClosestEnemy())
            if (_distanceToEnemy <= _isFightModeDistance)
                newState = State.FightMode;

        if (_isChasingEnemy && _isEnemyFightMode)
            if (distanceToPlayer <= _isChasingDistance)
                newState = State.ChasingToPlayer;

        if (_isChasingEnemy && _isEnemyFightMode && FindClosestEnemy() && _isClosestEnemyDead == false)
            if (_distanceToEnemy <= _isChasingDistance)
                newState = State.ChasingToEnemy;

        if (_isAttackingEnemy && _isEnemyFightMode)
            if (distanceToPlayer <= _isAttackingDistane || distanceToPortal <= _isAttackingDistane)
                newState = State.Attacking;

        if (_isAttackingEnemy && _isEnemyFightMode && FindClosestEnemy() && _isClosestEnemyDead == false)
            if (_distanceToEnemy <= _isAttackingDistane)
                newState = State.Attacking;

        if (Player.Instance.IsPlayerDead())
            newState = State.WalkToPortal;

        if (newState != _currentState)
            if (newState == State.Attacking)
                _navMeshAgent.ResetPath();

        _currentState = newState;
    }

    private void ChasingToPlayerTarget()
    {
        _navMeshAgent.SetDestination(Player.Instance.transform.position);
        _targetEnemy = Player.Instance.transform;
        RotationEnemy(_targetEnemy);
    }

    private void ChasingToEnemyTarget()
    {
        _navMeshAgent.SetDestination(_targetEnemy.position);
        RotationEnemy(_targetEnemy);
    }

    private void WalkToPortalTarget()
    {
        _navMeshAgent.SetDestination(PortalPlayer.Instance.transform.position);
        _targetEnemy = PortalPlayer.Instance.transform;
        RotationEnemy(_targetEnemy);
    }

    private void FightModeOn()
    {
        _isEnemyFightMode = true;
        if (_activeWeapon.tag == "Sword")
        {
            Sword sword = _activeWeapon.GetComponent<Sword>();
            sword.FightMode();
        }
    }

    private void AttackingTarget()
    {
        if (_targetEnemy != null)
        {
            RotationEnemy(_targetEnemy);
            if (_activeWeapon.tag == "Sword")
            {
                Sword sword = _activeWeapon.GetComponent<Sword>();
                if (transform.position.y > _targetEnemy.position.y)
                {
                    if (Time.time > _nextAttackTime)
                    {
                        sword.AttackDown();
                        _nextAttackTime = Time.time + sword.SwordAttackRate();
                    }
                }
                else
                {
                    if (Time.time > _nextAttackTime)
                    {
                        sword.AttackTop();
                        _nextAttackTime = Time.time + sword.SwordAttackRate();
                    }
                }
            }
        }
    }

    private void RotationEnemy(Transform targetEnemy)
    {
        _rotator.SetCharacterPos(transform.position);
        _rotator.SetTargetPos(targetEnemy.position);
        _rotator.Update(false);
    }
    // ----------------------------------
}
