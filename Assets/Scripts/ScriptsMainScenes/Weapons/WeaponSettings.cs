using System.Collections.Generic;
using UnityEngine;

namespace Outpost.WeaponSettings
{
    public static class WeaponSettings
    {

        // Создаем структуру для хранения настроек оружия
        public struct WeaponSetting
        {
            public int ItemIndex;
            public Vector3 LocalPosition;
            public float RotationAngle;
        }

        // Добавляем словарь с настройками для каждого типа оружия
        public static Dictionary<string, WeaponSetting> weaponSettings = new Dictionary<string, WeaponSetting>
        {
            {"Sword", new WeaponSetting {ItemIndex = 3, LocalPosition = new Vector3(-0.354f, 1.415f, 0), RotationAngle = -192}},
            {"Pickaxe", new WeaponSetting {ItemIndex = 4, LocalPosition = new Vector3(-0.022f, 0.73f, 0), RotationAngle = 0}},
            {"Axe", new WeaponSetting {ItemIndex = 5, LocalPosition = new Vector3(-0.022f, 0.73f, 0), RotationAngle = 0}}
        };
    }
}

