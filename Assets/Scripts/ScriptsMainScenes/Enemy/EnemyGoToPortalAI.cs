using UnityEngine;
using UnityEngine.AI;

public class EnemyGoToPortalAI : MonoBehaviour
{
    // Настройки через инспектор
    [Header("Main parameters")]
    [SerializeField] private float attackDistance = 2f;      // Расстояние для атаки
    [SerializeField] private float weaponDrawDistance = 3f;  // Расстояние для извлечения оружия
    [SerializeField] private float detectionRadius = 5f;     // Радиус обнаружения

    [HideInInspector]
    public bool isEnemyRunning // Опредедляем движется ли противник для анимации
    {
        get
        {
            if (navAgent.velocity == Vector3.zero)
                return false;
            else
                return true;
        }
    }

    private NavMeshAgent navAgent;
    private GameObject _activeWeapon;
    private DirectionalRotator _rotator;
    private Transform currentTarget;
    private EnemyState currentState;

    private bool isWeaponDrawn = false;
    private float _nextAttackTime;

    // Возможные состояния
    private enum EnemyState
    {
        MovingToPortal,
        ChasingPlayer,
        ChasingAllie
    }

    private void Awake()
    {
        _rotator = new DirectionalRotator(null, transform);
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    private void Start()
    {
        currentTarget = PortalPlayer.Instance.transform;
        currentState = EnemyState.MovingToPortal;
        _activeWeapon = transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        UpdateState();
        CheckTargets();
        RotationEnemy();

        if (!isWeaponDrawn)
            HandleWeapon();
    }

    private void CheckTargets()
    {
        // Проверяем объекты в радиусе обнаружения
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(
        transform.position,
        detectionRadius,
        LayerMask.GetMask("Player", "Allies"));

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.CompareTag("Player"))
            {
                currentTarget = hit.transform;
                currentState = EnemyState.ChasingPlayer;
                return;
            }
            else if (hit.CompareTag("Allie"))
            {
                currentTarget = hit.transform;
                currentState = EnemyState.ChasingAllie;
                return;
            }
        }

        // Если никого не нашли, возвращаемся к порталу
        currentTarget = PortalPlayer.Instance.transform;
        currentState = EnemyState.MovingToPortal;
    }

    private void UpdateState()
    {
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

        switch (currentState)
        {
            case EnemyState.MovingToPortal:
                StartMovement();
                navAgent.SetDestination(currentTarget.position);
                break;

            case EnemyState.ChasingPlayer:
                StartMovement();
                navAgent.SetDestination(currentTarget.position);
                break;

            case EnemyState.ChasingAllie:
                StartMovement();
                navAgent.SetDestination(currentTarget.position);
                break;
        }

        // Проверяем расстояние до цели и при необходимости атакуем
        if (distanceToTarget <= attackDistance)
        {
            StopMovement();
            Attacking();
        }
    }

    private void HandleWeapon()
    {
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

        if (distanceToTarget <= weaponDrawDistance)
        {
            DrawWeapon();
            isWeaponDrawn = true;
        }

    }

    private void StopMovement()
    {
        navAgent.isStopped = true;
    }

    private void StartMovement()
    {
        navAgent.isStopped = false;
    }

    private void DrawWeapon()
    {
        IWeapon weapon = _activeWeapon.GetComponent<IWeapon>();
        weapon.FightMode();
    }

    private void Attacking()
    {
        IWeapon weapon = _activeWeapon.GetComponent<IWeapon>();
        if (transform.position.y > currentTarget.position.y)
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

    private void RotationEnemy()
    {
        _rotator.SetCharacterPos(transform.position);
        _rotator.SetTargetPos(currentTarget.position);
        _rotator.Update(false);
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса обнаружения в редакторе
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
