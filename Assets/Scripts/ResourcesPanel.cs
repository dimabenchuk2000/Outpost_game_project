using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPanel : MonoBehaviour
{
    public static ResourcesPanel Instance;

    // Поле переменных
    [SerializeField] private Text _textCountTree;
    [SerializeField] private Text _textCountStone;
    [SerializeField] private Text _textCountCopperOre;
    [SerializeField] private Text _textCountCopper;
    [SerializeField] private Text _textCountIronOre;
    [SerializeField] private Text _textCountIron;
    [SerializeField] private Text _textCountTitanOre;
    [SerializeField] private Text _textCountTitan;
    [SerializeField] private Text _textCountDiamondOre;
    [SerializeField] private Text _textCountDiamond;
    [SerializeField] private Text _textCountCoin;
    [SerializeField] private Text _textCountCoal;

    private Dictionary<string, Text> resourceTexts;
    // ----------------------------------

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        resourceTexts = new Dictionary<string, Text>
        {
            { "Tree", _textCountTree },
            { "Stone", _textCountStone },
            { "CopperOre", _textCountCopperOre },
            { "Copper", _textCountCopper },
            { "IronOre", _textCountIronOre },
            { "Iron", _textCountIron },
            { "TitanOre", _textCountTitanOre },
            { "Titan", _textCountTitan },
            { "DiamondOre", _textCountDiamondOre },
            { "Diamond", _textCountDiamond },
            { "Coin", _textCountCoin },
            { "Coal", _textCountCoal }
        };
    }

    private void Start()
    {
        UpdateResourceList();
    }

    public void AddCount(string resource)
    {
        if (resourceTexts.TryGetValue(resource, out Text resourceText))
        {
            int i = int.Parse(resourceText.text);
            i += 1;
            resourceText.text = i.ToString();

            GameData.resourceCounts[resource] += 1;
        }
        else
        {
            Debug.LogWarning($"Resource '{resource}' not found.");
        }
    }

    public void UpdateResourceList()
    {
        foreach (string key in GameData.resourceCounts.Keys)
        {
            if (resourceTexts.TryGetValue(key, out Text resourceText))
            {
                resourceText.text = GameData.resourceCounts[key].ToString();
            }
        }
    }
}
