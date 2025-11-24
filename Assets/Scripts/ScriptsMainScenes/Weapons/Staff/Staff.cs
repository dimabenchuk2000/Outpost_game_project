using System;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private StaffSO _staffSO;

    // Поле событий
    public event EventHandler OnFightMode;
    public event EventHandler OnAttack;
    // ----------------------------------

    public void Attack(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Normal:
                OnAttack?.Invoke(this, EventArgs.Empty); // ---> StaffVisual;
                break;
            case AttackType.Additional:
                OnAttack?.Invoke(this, EventArgs.Empty); // ---> StaffVisual;
                break;
        }
    }

    public void FightMode()
    {
        OnFightMode?.Invoke(this, EventArgs.Empty); // ---> StaffVisual
    }

    public float GetAttackRate()
    {
        return _staffSO.staffAttackRate;
    }

    public int GetDamage()
    {
        return _staffSO.staffDamage;
    }
}
