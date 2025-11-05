using System;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    // Поле переменных
    [SerializeField] private SwordsSO _swordsSO;

    private int _swordDamage;
    private float _swordSpeedAttack;
    // ----------------------------------

    // Поле событий
    public event EventHandler OnFightMode;
    public event EventHandler OnAttackTop;
    public event EventHandler OnAttackDown;
    // ----------------------------------

    private void Start()
    {
        _swordDamage = _swordsSO.swordDamage;
        _swordSpeedAttack = _swordsSO.swordSpeedAttack;
    }

    // Поле публичных методов
    public void Attack(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Normal:
                AttackTop();
                break;
            case AttackType.Additional:
                AttackDown();
                break;
        }
    }

    public void FightMode()
    {
        OnFightMode?.Invoke(this, EventArgs.Empty); // ---> SwordVisual
    }

    public float GetAttackRate()
    {
        return _swordsSO.swordAttackRate;
    }

    public int SwordDamage() => _swordDamage;
    public float SwordSpeedAttack() => _swordSpeedAttack;

    private void AttackTop()
    {
        OnAttackTop?.Invoke(this, EventArgs.Empty); // ---> SwordVisual SwordWaveVisual
    }

    private void AttackDown()
    {
        OnAttackDown?.Invoke(this, EventArgs.Empty); // ---> SwordVisual SwordWaveVisual
    }
}
