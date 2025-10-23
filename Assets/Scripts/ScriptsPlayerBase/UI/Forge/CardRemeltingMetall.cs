using UnityEngine;
using UnityEngine.UI;

public class CardRemeltingMetall : MonoBehaviour
{
    [SerializeField] private int requiredCost_MetalOre;
    [SerializeField] private int requiredCost_Coal;
    [SerializeField] private int requiredCost_Coin;

    [SerializeField] private Text factCountMetalOre;
    [SerializeField] private Text factCountCoal;
    [SerializeField] private Text factCountCoin;

    [SerializeField] private Text textRequiredCost_Metal;
    [SerializeField] private Text textRequiredCost_Coal;
    [SerializeField] private Text textRequiredCost_Coin;

    [SerializeField] private string resultingMetal;

    private void Start()
    {
        textRequiredCost_Metal.text = requiredCost_MetalOre.ToString();
        textRequiredCost_Coal.text = requiredCost_Coal.ToString();
        textRequiredCost_Coin.text = requiredCost_Coin.ToString();
    }

    public void RemeltingMetall()
    {
        if (CanRemelting())
        {
            GameData.resourceCounts[resultingMetal] += 1;
            DeductResources();
        }
        else
        {
            Debug.Log("Нехватает ресурсов");
        }
    }

    private bool CanRemelting()
    {
        return int.Parse(factCountMetalOre.text) >= requiredCost_MetalOre &&
        int.Parse(factCountCoal.text) >= requiredCost_Coal &&
        int.Parse(factCountCoin.text) >= requiredCost_Coin;
    }

    private void DeductResources()
    {
        int differenceMetal = int.Parse(factCountMetalOre.text) - requiredCost_MetalOre;
        int differenceCoal = int.Parse(factCountCoal.text) - requiredCost_Coal;
        int differenceCoin = int.Parse(factCountCoin.text) - requiredCost_Coin;

        GameData.resourceCounts[resultingMetal + "Ore"] = differenceMetal;
        GameData.resourceCounts["Coal"] = differenceCoal;
        GameData.resourceCounts["Coin"] = differenceCoin;

        ResourcesPanel.Instance.UpdateResourceList();
    }
}
