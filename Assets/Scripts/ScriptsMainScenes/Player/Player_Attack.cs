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
                    GameObject activeWeapon = ActiveWeapon.Instance.CheckActiveWeapon();
                    if (activeWeapon.tag == "Sword")
                        activeWeapon.GetComponent<Sword>().FightMode();
                    if (activeWeapon.tag == "Bow")
                        activeWeapon.GetComponent<Bow>().FightMode();
                    if (activeWeapon.tag == "Tools")
                        activeWeapon.GetComponent<Tools>().FightMode();
                }

            }
        }

        public static void GameInput_OnPlayerAttackTop(object sender, EventArgs e)
        {
            if (_isPlayerFightMode && ActiveWeapon.Instance.transform.childCount > 0 && !Player.Instance.isPlayerTPBase)
            {
                GameObject activeWeapon = ActiveWeapon.Instance.CheckActiveWeapon();
                if (activeWeapon.tag == "Sword")
                {
                    Sword sword = activeWeapon.GetComponent<Sword>();
                    if (Time.time > _nextAttackTime)
                    {
                        sword.AttackTop();
                        _nextAttackTime = Time.time + sword.SwordAttackRate();
                    }
                }

                if (activeWeapon.tag == "Bow")
                {
                    Bow bow = activeWeapon.GetComponent<Bow>();
                    if (Time.time > _nextAttackTime)
                    {
                        bow.Attack();
                    }
                }

                if (activeWeapon.tag == "Tools")
                {
                    Tools tools = activeWeapon.GetComponent<Tools>();
                    if (Time.time > _nextAttackTime)
                    {
                        tools.Extraction();
                        _nextAttackTime = Time.time + tools.ToolsExtractionRate();
                    }
                }
            }
        }

        public static void GameInput_OnPlayerAttackDown(object sender, EventArgs e)
        {
            if (_isPlayerFightMode && ActiveWeapon.Instance.transform.childCount > 0 && !Player.Instance.isPlayerTPBase)
            {
                GameObject activeWeapon = ActiveWeapon.Instance.CheckActiveWeapon();
                if (activeWeapon.tag == "Sword")
                {
                    Sword sword = activeWeapon.GetComponent<Sword>();
                    if (Time.time > _nextAttackTime)
                    {
                        sword.AttackDown();
                        _nextAttackTime = Time.time + sword.SwordAttackRate();
                    }

                }
            }
        }
        // ----------------------------------
    }
}
