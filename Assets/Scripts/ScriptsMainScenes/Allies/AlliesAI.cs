using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AlliesEntity))]

public class AlliesAI : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private List<EnemyEntity> _enemys;
    [SerializeField] private State _startingState;

    [SerializeField] private float _isFightModeDistance = 5f;
    [SerializeField] private float _isChasingDistance = 5f;
    [SerializeField] private float _isAttackingDistane = 2f;

    private NavMeshAgent _navMeshAgent;
    private AlliesEntity _alliesEntity;

    private State _currentState;
    private bool _isAlliesFightMode = false;
    private GameObject _activeWeapon;
    private Transform _targetAllies;
    private float _distanceToEnemy;
    private EnemyEntity _closestEnemy;
    private bool _isClosestEnemyDead;
    private float _nextAttackTime;
    private Vector2 _startPosition;

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

    // Список сотояний союзника
    private enum State
    {
        Idle,
        Roaming,
        Chasing,
        FightMode,
        Attacking,
        Death
    } // ----------------------------------

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _alliesEntity = GetComponent<AlliesEntity>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        _startPosition = transform.position;
    }

    private void Start()
    {
        _currentState = _startingState;
        _activeWeapon = transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        if (_alliesEntity.IsAlliesDead() == false)
        {
            if (FindClosestEnemy())
            {
                CheckCurrentState();
                _targetAllies = _closestEnemy.transform;
                StateHandler();
            }
            else
            {
                _targetAllies = null;
                CheckCurrentState();
                StateHandler();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity Enemy))
        {
            _enemys.Add(Enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity Enemy))
        {
            _enemys.Remove(Enemy);
        }
    }

    // Поле приватных методов
    private EnemyEntity FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (EnemyEntity enemy in _enemys)
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
            case State.Idle:
                IdleTarget();
                CheckCurrentState();
                break;
            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;
            case State.FightMode:
                FightModeOn();
                CheckCurrentState();
                break;
            case State.Attacking:
                RotationEnemy(_targetAllies);
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
            _isClosestEnemyDead = _closestEnemy.IsEnemyDead();
        }

        State newState = State.Idle;

        if (FindClosestEnemy() == null)
            newState = State.Idle;

        if (_isAlliesFightMode == false && FindClosestEnemy())
            if (_distanceToEnemy <= _isFightModeDistance)
                newState = State.FightMode;

        if (_isAlliesFightMode && FindClosestEnemy() && _isClosestEnemyDead == false)
            if (_distanceToEnemy <= _isChasingDistance)
                newState = State.Chasing;

        if (_isAlliesFightMode && FindClosestEnemy() && _isClosestEnemyDead == false)
            if (_distanceToEnemy <= _isAttackingDistane)
                newState = State.Attacking;

        if (newState != _currentState)
            if (newState == State.Attacking)
                _navMeshAgent.ResetPath();

        _currentState = newState;
    }

    private void IdleTarget()
    {
        _navMeshAgent.SetDestination(_startPosition);
    }

    private void ChasingTarget()
    {
        if (_targetAllies != null)
        {
            _navMeshAgent.SetDestination(_targetAllies.position);
            RotationEnemy(_targetAllies);
        }
    }

    private void FightModeOn()
    {
        _isAlliesFightMode = true;
        if (_activeWeapon.tag == "Sword")
        {
            Sword sword = _activeWeapon.GetComponent<Sword>();
            sword.FightMode();
        }
    }

    private void AttackingTarget()
    {
        if (_targetAllies != null)
        {
            if (_activeWeapon.tag == "Sword")
            {
                Sword sword = _activeWeapon.GetComponent<Sword>();
                if (transform.position.y > _targetAllies.position.y)
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

    private void RotationEnemy(Transform targetAllies)
    {
        Vector3 targetAlliesPos = targetAllies.position;
        Vector3 alliesPos = transform.position;

        if (targetAlliesPos.x < alliesPos.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    // ----------------------------------
}
