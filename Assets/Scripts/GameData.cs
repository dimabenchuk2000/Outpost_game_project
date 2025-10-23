using System.Collections.Generic;

public static class GameData
{
    public static int countBarracks = 1;
    public static int globalHP = 3;

    public static Dictionary<string, int> resourceCounts = new Dictionary<string, int>
    {
        { "Tree", 1000 },
        { "Stone", 1000 },
        { "CopperOre", 10 },
        { "Copper", 100 },
        { "IronOre", 10 },
        { "Iron", 100 },
        { "TitanOre", 10 },
        { "Titan", 100 },
        { "DiamondOre", 10 },
        { "Diamond", 100 },
        { "Coin", 1500 },
        { "Coal", 100 },
    };

    public static Dictionary<string, int> buildingsLevel = new Dictionary<string, int>
    {
        { "TownHall", 1 },
        { "Forge", 1 },
        {"Barracks_1", 1},
        {"Barracks_2", 1},
        {"Barracks_3", 1},
        {"Barracks_4", 1},
        {"Barracks_5", 1},
        {"Barracks_6", 1},
    };

    public static Dictionary<string, bool> improvementList = new Dictionary<string, bool>
    {
        {"Dash", false},
        {"Inventory_12_slots", true},
        {"Run", false},
        {"Inventory_15_slots", true},
    };

    public static Dictionary<string, int> weaponLevel = new Dictionary<string, int>
    {
        { "Sword", 1 },
        { "Pickaxe", 1 },
        { "Axe", 1 },
        { "Mercenary_1_SwordLevel", 1 },
        { "Mercenary_2_SwordLevel", 1 },
        { "Mercenary_3_SwordLevel", 1 },
        { "Mercenary_4_SwordLevel", 1 },
        { "Mercenary_5_SwordLevel", 1 },
        { "Mercenary_6_SwordLevel", 1 },
    };

    public static Dictionary<string, bool> mercenaryList = new Dictionary<string, bool>
    {
        { "Mercenary_1", true },
        { "Mercenary_2", false },
        { "Mercenary_3", false },
        { "Mercenary_4", false },
        { "Mercenary_5", false },
        { "Mercenary_6", false },
    };
}
