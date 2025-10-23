using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private SwordsSO _swordsSO;

    private float _swordAttackRate;
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
        _swordAttackRate = _swordsSO.swordAttackRate;
        _swordDamage = _swordsSO.swordDamage;
        _swordSpeedAttack = _swordsSO.swordSpeedAttack;
    }

    // Поле публичных методов
    public float SwordAttackRate() => _swordAttackRate;
    public int SwordDamage() => _swordDamage;
    public float SwordSpeedAttack() => _swordSpeedAttack;

    public void FightMode()
    {
        OnFightMode?.Invoke(this, EventArgs.Empty); // ---> SwordVisual
    }

    public void AttackTop()
    {
        OnAttackTop?.Invoke(this, EventArgs.Empty); // ---> SwordVisual SwordWaveVisual
    }

    public void AttackDown()
    {
        OnAttackDown?.Invoke(this, EventArgs.Empty); // ---> SwordVisual SwordWaveVisual
    }
}
