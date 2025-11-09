using UnityEngine;
using UnityEngine.AI;

public class AlliesAI : MonoBehaviour
{
    // Настройки через инспектор
    [Header("Main parameters")]
    [SerializeField] private float _attackDistance = 2f;      // Расстояние для атаки
    [SerializeField] private float _weaponDrawDistance = 3f;  // Расстояние для извлечения оружия
    [SerializeField] private float _detectionRadius = 5f;     // Радиус обнаружения

    [HideInInspector]
    public bool isAlliesRunning // Опредедляем движется ли союзник для анимации
    {
        get
        {
            if (_navAgent.velocity == Vector3.zero)
                return false;
            else
                return true;
        }
    }

    private NavMeshAgent _navAgent;
    private GameObject _activeWeapon;
    private DirectionalRotator _rotator;
    private Transform _currentTarget;
    private EnemyState _currentState;

    private bool _isWeaponDrawn = false;
    private float _nextAttackTime;
    private Vector2 _startPosition;

    // Возможные состояния
    private enum EnemyState
    {
        Stopped,
        GoToStartPos,
        ChasingEnemy
    }

    private void Awake()
    {
        _rotator = new DirectionalRotator(null, transform);
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.updateRotation = false;
        _navAgent.updateUpAxis = false;
    }

    private void Start()
    {
        _currentState = EnemyState.GoToStartPos;
        _activeWeapon = transform.GetChild(1).gameObject;
        _startPosition = transform.position;
    }

    private void Update()
    {
        CheckTargets();
        UpdateState();
        RotationAllie();

        if (!_isWeaponDrawn)
            HandleWeapon();
    }

    private void CheckTargets()
    {
        // Проверяем объекты в радиусе обнаружения
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(
        transform.position,
        _detectionRadius,
        LayerMask.GetMask("Enemy"));

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.CompareTag("Enemy") && !Player.Instance.isPlayerDead)
            {
                _currentTarget = hit.transform;
                _currentState = EnemyState.ChasingEnemy;
                return;
            }
        }

        // Если игрок погиб, все союзники бездействуют
        if (Player.Instance.isPlayerDead)
        {
            _currentState = EnemyState.Stopped;
            return;
        }

        // Если никого не нашли, возвращаемся к стартовой позиции
        _currentTarget = PortalPlayer.Instance.transform;
        _currentState = EnemyState.GoToStartPos;
    }

    private void UpdateState()
    {
        float distanceToTarget = Vector3.Distance(transform.position, _currentTarget.position);

        switch (_currentState)
        {
            case EnemyState.Stopped:
                StopMovement();
                break;

            case EnemyState.GoToStartPos:
                StartMovement();
                _navAgent.SetDestination(_startPosition);
                break;

            case EnemyState.ChasingEnemy:
                StartMovement();
                _navAgent.SetDestination(_currentTarget.position);
                break;
        }

        // Проверяем расстояние до цели и при необходимости атакуем
        if (distanceToTarget <= _attackDistance && !Player.Instance.isPlayerDead)
        {
            StopMovement();
            Attacking();
        }
    }

    private void HandleWeapon()
    {
        float distanceToTarget = Vector3.Distance(transform.position, _currentTarget.position);

        if (distanceToTarget <= _weaponDrawDistance)
        {
            DrawWeapon();
            _isWeaponDrawn = true;
        }

    }

    private void StopMovement() => _navAgent.isStopped = true;

    private void StartMovement() => _navAgent.isStopped = false;

    private void DrawWeapon()
    {
        IWeapon weapon = _activeWeapon.GetComponent<IWeapon>();
        weapon.FightMode();
    }

    private void Attacking()
    {
        IWeapon weapon = _activeWeapon.GetComponent<IWeapon>();
        if (transform.position.y > _currentTarget.position.y)
        {
            if (Time.time > _nextAttackTime)
            {
                weapon.Attack(AttackType.Additional);
                _nextAttackTime = Time.time + weapon.GetAttackRate();
            }
        }
        else
        {
            if (Time.time > _nextAttackTime)
            {
                weapon.Attack(AttackType.Normal);
                _nextAttackTime = Time.time + weapon.GetAttackRate();
            }
        }
    }

    private void RotationAllie()
    {
        _rotator.SetCharacterPos(transform.position);
        _rotator.SetTargetPos(_currentTarget.position);
        _rotator.Update(false);
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса обнаружения в редакторе
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
