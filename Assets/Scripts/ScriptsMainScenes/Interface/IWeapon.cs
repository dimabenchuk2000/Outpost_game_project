using UnityEngine;

public interface IWeapon
{
    void Attack(AttackType attackType);
    void FightMode();
    float GetAttackRate();
}

// 2. Создаем перечисление для типов атак
public enum AttackType
{
    Normal,
    Additional
}
