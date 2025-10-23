using System;
using UnityEngine;

[RequireComponent(typeof(KnockBack))]

public class EnemyEntity : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private EnemySO _enemySO;

    private KnockBack _knockBack;

    private int _currentHealth;
    private int _maxCountCoin;
    private bool _isEnemyDead = false;
    // ----------------------------------

    // Поле событий
    public event EventHandler OnEnemyTakeDamage;
    public event EventHandler OnEnemyDead;
    // ----------------------------------

    private void Awake()
    {
        _knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        _currentHealth = _enemySO.enemyHealth;
        _maxCountCoin = _enemySO.maxCountCoin;
    }

    // Поле публичных методов
    public bool IsEnemyDead() => _isEnemyDead;

    public void TakeDamage(int damage, Transform sourceDamage)
    {
        if (_isEnemyDead == false)
        {
            _currentHealth -= damage;
            _knockBack.GetKnockBack(sourceDamage);
            OnEnemyTakeDamage?.Invoke(this, EventArgs.Empty); // ---> AncientVisual
            DetectDeath();
        }
    }
    // ----------------------------------

    // Поле приватных методов
    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            _isEnemyDead = true;
            _knockBack.StopKnockBackMovement();

            int i = UnityEngine.Random.Range(0, _maxCountCoin);
            for (int j = 0; j <= i; j++)
            {
                GameObject obj = Instantiate(DataBase.Instance._items[10].obj);
                obj.transform.localPosition = transform.position;
            }

            OnEnemyDead?.Invoke(this, EventArgs.Empty); // ---> AncientVisual
        }
    }
    // ----------------------------------
}
