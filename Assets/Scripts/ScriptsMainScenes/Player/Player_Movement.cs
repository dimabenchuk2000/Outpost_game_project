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
        // ----------------------------------

        public static void GameInput_OnPlayerRunPerformed(object sender, EventArgs e)
        {
            // if (Player.Instance.currentEnergy >= 1)
            // {
            //     Player.Instance.isShiftHeld = true;
            //     Player.Instance.LossEnergyRunning();
            //     RunCheck();
            // }
        }

        public static void GameInput_OnPlayerRunCancaled(object sender, EventArgs e)
        {
            // Player.Instance.isShiftHeld = false;
            // Player.Instance.AddEnergy();
            // RunCheck();
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

