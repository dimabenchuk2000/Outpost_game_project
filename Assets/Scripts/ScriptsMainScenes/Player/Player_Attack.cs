using System;
using UnityEngine;

namespace Outpost.Player_Attack
{
    public static class Player_Attack
    {
        // Поле переменных
        public static bool _isPlayerFightMode;
        private static float _nextAttackTime;
        // ----------------------------------

        // Поле публичных методов
        public static bool IsPlayerFightMode() => _isPlayerFightMode;

        public static void GameInput_OnPlayerFightMode(object sender, EventArgs e)
        {
            if (ActiveWeapon.Instance.transform.childCount != 0 && !Player.Instance.isPlayerTPBase)
            {
                if (_isPlayerFightMode)
                    Player.Instance.IsFightModeOff();
                else
                    _isPlayerFightMode = true;

                if (ActiveWeapon.Instance.transform.childCount > 0)
                {
                    IWeapon weapon = ActiveWeapon.Instance.CheckActiveWeapon().GetComponent<IWeapon>();
                    weapon.FightMode();
                }

            }
        }

        public static void GameInput_OnPlayerAttackTop(object sender, EventArgs e)
        {
            if (_isPlayerFightMode && ActiveWeapon.Instance.transform.childCount > 0 && !Player.Instance.isPlayerTPBase)
            {
                IWeapon weapon = ActiveWeapon.Instance.CheckActiveWeapon().GetComponent<IWeapon>();

                if (Time.time > _nextAttackTime)
                {
                    weapon.Attack(AttackType.Normal);
                    _nextAttackTime = Time.time + weapon.GetAttackRate();
                }
            }
        }

        public static void GameInput_OnPlayerAttackDown(object sender, EventArgs e)
        {
            if (_isPlayerFightMode && ActiveWeapon.Instance.transform.childCount > 0 && !Player.Instance.isPlayerTPBase)
            {
                IWeapon weapon = ActiveWeapon.Instance.CheckActiveWeapon().GetComponent<IWeapon>();

                if (Time.time > _nextAttackTime)
                {
                    weapon.Attack(AttackType.Additional);
                    _nextAttackTime = Time.time + weapon.GetAttackRate();
                }
            }
        }
        // ----------------------------------
    }
}
