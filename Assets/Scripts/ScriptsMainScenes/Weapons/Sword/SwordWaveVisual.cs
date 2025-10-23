using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class SwordWaveVisual : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private Sword _sword;

    private Animator _animator;

    private const string IS_ATTACK_TOP = "isAttackTop";
    private const string IS_ATTACK_DOWN = "isAttackDown";
    private const string IS_SPEED_WAWE = "isSpeedWave";
    // ----------------------------------

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _sword.OnAttackTop += Sword_OnAttackTop;
        _sword.OnAttackDown += Sword_OnAttackDown;
    }

    private void OnDestroy()
    {
        _sword.OnAttackTop -= Sword_OnAttackTop;
    }

    // Поле приватных методов
    private void Sword_OnAttackTop(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_ATTACK_TOP);
        _animator.SetFloat(IS_SPEED_WAWE, _sword.SwordSpeedAttack());
    }

    private void Sword_OnAttackDown(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_ATTACK_DOWN);
        _animator.SetFloat(IS_SPEED_WAWE, _sword.SwordSpeedAttack());
    }
    // ----------------------------------
}
