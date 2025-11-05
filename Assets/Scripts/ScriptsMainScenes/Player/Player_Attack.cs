using System;
using UnityEngine;

namespace Outpost.Player_Attack
{
    public static class Player_Attack
    {
        // Поле переменных
        public static bool _isPlayerFightMode;
        private static float _nextAttackTime;
        private static IWeapon _currentWeapon;
        // ----------------------------------

        // Инициализация статических полей
        static Player_Attack()
        {
            _isPlayerFightMode = false;
            _nextAttackTime = 0f;
        }

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
                    _currentWeapon = ActiveWeapon.Instance.CheckActiveWeapon().GetComponent<IWeapon>();
                    _currentWeapon.FightMode();
                }

            }
        }

        public static void GameInput_OnPlayerAttackTop(object sender, EventArgs e)
        {
            if (_isPlayerFightMode && ActiveWeapon.Instance.transform.childCount > 0 && !Player.Instance.isPlayerTPBase)
            {
                _currentWeapon = ActiveWeapon.Instance.CheckActiveWeapon().GetComponent<IWeapon>();

                if (Time.time > _nextAttackTime)
                {
                    _currentWeapon.Attack(AttackType.Normal);
                    _nextAttackTime = Time.time + _currentWeapon.GetAttackRate();
                }
            }
        }

        public static void GameInput_OnPlayerAttackDown(object sender, EventArgs e)
        {
            if (_isPlayerFightMode && ActiveWeapon.Instance.transform.childCount > 0 && !Player.Instance.isPlayerTPBase)
            {
                _currentWeapon = ActiveWeapon.Instance.CheckActiveWeapon().GetComponent<IWeapon>();

                if (Time.time > _nextAttackTime)
                {
                    _currentWeapon.Attack(AttackType.Additional);
                    _nextAttackTime = Time.time + _currentWeapon.GetAttackRate();
                }
            }
        }

        public static void SubscribeToEvents()
        {
            GameInput.Instance.OnPlayerFightMode += GameInput_OnPlayerFightMode;
            GameInput.Instance.OnPlayerAttackTop += GameInput_OnPlayerAttackTop;
            GameInput.Instance.OnPlayerAttackDown += GameInput_OnPlayerAttackDown;
        }

        // Метод для отписки от событий
        public static void UnsubscribeFromEvents()
        {
            GameInput.Instance.OnPlayerFightMode -= GameInput_OnPlayerFightMode;
            GameInput.Instance.OnPlayerAttackTop -= GameInput_OnPlayerAttackTop;
            GameInput.Instance.OnPlayerAttackDown -= GameInput_OnPlayerAttackDown;
        }
        // ----------------------------------
    }
}
