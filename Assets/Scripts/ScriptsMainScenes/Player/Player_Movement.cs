using System;
using UnityEngine;

namespace Outpost.Player_Movement
{
    public static class Player_Movement
    {
        // Поле переменных
        public static float movementSpeed = 5f;
        public static float dashMultiplier = 4f;
        public static float dashTime = 0.1f;

        private static bool _isPlayerRunning = false;

        private static Rigidbody2D _rb = Player.Instance.Rigidbody2D();
        // ----------------------------------

        public static bool IsPlayerRunning() => _isPlayerRunning;

        public static Vector3 GetPlayerPosition()
        {
            Vector3 playerPosition = Camera.main.WorldToScreenPoint(Player.Instance.transform.position);
            return playerPosition;
        }

        public static void PlayerMove()
        {
            if (_rb != null && !Player.Instance.isPlayerTPBase)
            {
                if (Player.Instance._knockBack.IsKnockBack() == false && Player.Instance.IsPlayerDead() == false)
                {
                    Vector2 vectorDirectionMovement = Player.Instance.VectorDirectionMovement();
                    _rb.MovePosition(_rb.position + vectorDirectionMovement * (movementSpeed * Time.fixedDeltaTime));

                    if (Mathf.Abs(vectorDirectionMovement.x) > 0 || Mathf.Abs(vectorDirectionMovement.y) > 0)
                        _isPlayerRunning = true;
                    else
                        _isPlayerRunning = false;
                }
            }
            else
            {
                _rb = Player.Instance.Rigidbody2D();
            }
        }

        public static void GameInput_OnPlayerDashPerformed(object sender, EventArgs e)
        {
            if (!Player.Instance.isShiftHeld)
                Player.Instance.DashOn();
        }

        public static void GameInput_OnPlayerRunPerformed(object sender, EventArgs e)
        {
            if (Player.Instance.currentEnergy >= 1)
            {
                Player.Instance.isShiftHeld = true;
                Player.Instance.LossEnergyRunning();
                RunCheck();
            }
        }

        public static void GameInput_OnPlayerRunCancaled(object sender, EventArgs e)
        {
            Player.Instance.isShiftHeld = false;
            Player.Instance.AddEnergy();
            RunCheck();
        }

        public static void RunCheck()
        {
            switch (Player.Instance.isShiftHeld)
            {
                case true:
                    movementSpeed = 7f;
                    break;
                case false:
                    movementSpeed = 5f;
                    break;
            }
        }
    }
}

