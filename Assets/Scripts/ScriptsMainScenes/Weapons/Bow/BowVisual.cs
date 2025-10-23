using System;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class BowVisual : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private Bow _bow;

    private Animator _animator;

    private const string IS_FIGHT_MODE = "isFightMode";
    private const string IS_ATTACK = "isAttack";
    // ----------------------------------

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _bow.OnFightMode += Bow_OnFightMode;
        _bow.OnAttack += Bow_OnAttack;
    }

    private void OnDestroy()
    {
        _bow.OnFightMode -= Bow_OnFightMode;
        _bow.OnAttack -= Bow_OnAttack;
    }

    // Поле публичных методов
    public void CreateArrow()
    {
        Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
    }
    // ----------------------------------

    // Поле приватных методов
    private void Bow_OnFightMode(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_FIGHT_MODE);
    }

    private void Bow_OnAttack(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_ATTACK);
    }
    // ----------------------------------
}
