using System;
using UnityEngine;

[RequireComponent(typeof(KnockBack))]

public class AlliesEntity : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private int _mercenatyHealth = 30;

    private KnockBack _knockBack;
    private Animator _animator;

    private int _currentHealth;
    private bool _isAlliesDead = false;

    private const string IS_TAKE_DAMAGE = "isTakeDamage";
    private const string IS_DEAD = "isDead";
    // ----------------------------------

    private void Awake()
    {
        _knockBack = GetComponent<KnockBack>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentHealth = _mercenatyHealth;
    }

    // Поле публичных методов
    public bool IsAlliesDead() => _isAlliesDead;
    public void DestroyAllies() => Destroy(transform.gameObject);

    public void TakeDamage(int damage, Transform sourceDamage)
    {
        if (_isAlliesDead == false)
        {
            _currentHealth -= damage;
            _knockBack.GetKnockBack(sourceDamage);
            _animator.SetTrigger(IS_TAKE_DAMAGE);
            DetectDeath();
        }
    }
    // ----------------------------------

    // Поле приватных методов
    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            _isAlliesDead = true;
            _knockBack.StopKnockBackMovement();
            _animator.SetBool(IS_DEAD, true);
        }
    }
    // ----------------------------------
}
