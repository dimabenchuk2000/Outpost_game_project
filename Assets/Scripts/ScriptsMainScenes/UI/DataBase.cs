using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public static DataBase Instance;

    // Поле переменных
    public List<Item> _items = new List<Item>();

    [SerializeField] private GameObject stoneSword;
    [SerializeField] private GameObject copperSword;
    [SerializeField] private GameObject ironSword;
    [SerializeField] private GameObject titanSword;
    [SerializeField] private GameObject diamondSword;

    [SerializeField] private GameObject copperPickaxe;
    [SerializeField] private GameObject ironPickaxe;
    [SerializeField] private GameObject titanPickaxe;
    [SerializeField] private GameObject diamondPickaxe;

    [SerializeField] private GameObject copperAxe;
    [SerializeField] private GameObject ironAxe;
    [SerializeField] private GameObject titanAxe;
    [SerializeField] private GameObject diamondAxe;

    public Dictionary<string, int> resourceMap = new Dictionary<string, int>
    {
            { "Tree", 1 },
            { "Stone", 2 },
            { "CopperOre", 6 },
            { "IronOre", 7 },
            { "TitanOre", 8 },
            { "DiamondOre", 9 },
            { "Coin", 10 },
            { "Coal", 11 }
    };
    // ----------------------------------

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        switch (GameData.weaponLevel["Sword"])
        {
            case 1:
                break;
            case 2:
                _items[3].obj = copperSword;
                break;
            case 3:
                _items[3].obj = ironSword;
                break;
            case 4:
                _items[3].obj = titanSword;
                break;
            case 5:
                _items[3].obj = diamondSword;
                break;
            default:
                break;
        }

        switch (GameData.weaponLevel["Pickaxe"])
        {
            case 1:
                break;
            case 2:
                _items[4].obj = copperPickaxe;
                break;
            case 3:
                _items[4].obj = ironPickaxe;
                break;
            case 4:
                _items[4].obj = titanPickaxe;
                break;
            case 5:
                _items[4].obj = diamondPickaxe;
                break;
            default:
                break;
        }

        switch (GameData.weaponLevel["Axe"])
        {
            case 1:
                break;
            case 2:
                _items[5].obj = copperAxe;
                break;
            case 3:
                _items[5].obj = ironAxe;
                break;
            case 4:
                _items[5].obj = titanAxe;
                break;
            case 5:
                _items[5].obj = diamondAxe;
                break;
            default:
                break;
        }
    }

}

[System.Serializable]

public class Item
{
    public int id;
    public string name;
    public Sprite img;
    public GameObject obj;
}
