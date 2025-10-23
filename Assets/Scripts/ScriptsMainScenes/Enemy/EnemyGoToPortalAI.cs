using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyEntity))]

public class EnemyGoToPortalAI : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private List<AlliesEntity> _allies;
    [SerializeField] private State _startingState;

    [SerializeField] private float _isFightModeDistance = 7f;
    [SerializeField] private float _isChasingDistance = 5f;
    [SerializeField] private float _isAttackingDistane = 2f;

    private NavMeshAgent _navMeshAgent;
    private EnemyEntity _enemyEntity;

    private State _currentState;
    private GameObject _activeWeapon;
    private Transform _targetEnemy;
    private AlliesEntity _closestAllie;

    private bool _isEnemyFightMode = false;

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
        ChasingToPlayer,
        ChasingToAllie,
        WalkToPortal,
        FightMode,
        Attacking
    } // ----------------------------------

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyEntity = GetComponent<EnemyEntity>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
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
            if (FindClosestAllies())
            {
                _targetEnemy = _closestAllie.transform;
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
        if (collision.transform.TryGetComponent(out AlliesEntity Allie))
        {
            _allies.Add(Allie);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out AlliesEntity Allie))
        {
            _allies.Remove(Allie);
        }
    }

    // Поле приватных методов
    private AlliesEntity FindClosestAllies()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (AlliesEntity allie in _allies)
        {
            if (allie != null)
            {
                Vector3 diff = allie.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (distance > curDistance)
                {
                    _closestAllie = allie;
                    distance = curDistance;
                }
            }
        }
        return _closestAllie;
    }

    private void StateHandler()
    {
        switch (_currentState)
        {
            case State.Idle:
                AgentStop();
                break;
            case State.ChasingToPlayer:
                ChasingToPlayerTarget();
                CheckCurrentState();
                break;
            case State.ChasingToAllie:
                ChasingToAllieTarget();
                CheckCurrentState();
                break;
            case State.WalkToPortal:
                WalkToPortal();
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
        if (_targetEnemy == null)
        {
            _targetEnemy = PortalPlayer.Instance.transform;
        }

        float distanceToPlayer = Vector3.Distance(Player.Instance.transform.position, transform.position);
        float distanceToTraget = Vector3.Distance(_targetEnemy.position, transform.position);
        State newState = State.WalkToPortal;

        if (Player.Instance.isPlayerDead)
        {
            newState = State.Idle;
        }

        if (!_isEnemyFightMode && distanceToTraget <= _isFightModeDistance)
            newState = State.FightMode;

        if (!_isEnemyFightMode && distanceToPlayer <= _isFightModeDistance)
            newState = State.FightMode;

        if (_isEnemyFightMode && !Player.Instance.isPlayerDead)
        {
            if (_targetEnemy.tag == "Allie" && distanceToTraget <= _isChasingDistance)
                newState = State.ChasingToAllie;
            if (distanceToTraget <= _isAttackingDistane)
                newState = State.Attacking;
            if (distanceToPlayer <= _isChasingDistance && _targetEnemy.tag != "Allie")
                newState = State.ChasingToPlayer;
            if (distanceToPlayer <= _isAttackingDistane && _targetEnemy.tag != "Allie")
                newState = State.Attacking;
        }

        _currentState = newState;
    }

    private void AgentStop()
    {
        _navMeshAgent.speed = 0f;
    }

    private void ChasingToPlayerTarget()
    {
        _navMeshAgent.SetDestination(Player.Instance.transform.position);
        _targetEnemy = Player.Instance.transform;
        RotationEnemy(_targetEnemy);
    }

    private void ChasingToAllieTarget()
    {
        _navMeshAgent.SetDestination(_targetEnemy.position);
        RotationEnemy(_targetEnemy);
    }

    private void WalkToPortal()
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
        Vector3 targetEnemyPos = targetEnemy.position;
        Vector3 enemyPos = transform.position;

        if (targetEnemyPos.x < enemyPos.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    // ----------------------------------
}
