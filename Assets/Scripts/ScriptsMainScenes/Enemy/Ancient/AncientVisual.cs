using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AncientVisual : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private EnemyEntity _enemyEntity;
    [SerializeField] private EnemyGoToPortalAI _enemyAI;
    [SerializeField] private GameObject _enemy;

    private Animator _animator;

    private const string IS_TAKE_DAMAGE = "isTakeDamage";
    private const string IS_DEAD = "isDead";
    private const string IS_RUNNING = "isRunning";
    private const string IS_SPAWN = "isSpawn";
    // ----------------------------------

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _enemyEntity.OnEnemyTakeDamage += EnemyEntity_OnEnemyTakeDamage;
        _enemyEntity.OnEnemyDead += EnemyEntity_OnEnemyDead;
    }

    private void Update()
    {
        _animator.SetBool(IS_RUNNING, _enemyAI.isEnemyRunning);
    }

    private void OnDestroy()
    {
        _enemyEntity.OnEnemyTakeDamage -= EnemyEntity_OnEnemyTakeDamage;
        _enemyEntity.OnEnemyDead -= EnemyEntity_OnEnemyDead;
    }

    // Поле публичных методов
    public void DestroyEnemy()
    {
        Destroy(_enemy);
    }
    // ----------------------------------

    // Поле приватных методов
    private void EnemyEntity_OnEnemyTakeDamage(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_TAKE_DAMAGE);
    }

    private void EnemyEntity_OnEnemyDead(object sender, EventArgs e)
    {
        _animator.SetBool(IS_DEAD, true);
    }
    // ----------------------------------
}
