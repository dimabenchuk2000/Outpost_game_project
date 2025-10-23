using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUnbuiltBarracks : MonoBehaviour
{
    public static PanelUnbuiltBarracks Instance;

    // Поле переменных
    [SerializeField] private BuildingUpgradeCostSO buildingBuildCostSO;

    [SerializeField] private GameObject panelUnbuiltBarracks;

    [SerializeField] private Sprite spriteBarracks;
    [SerializeField] private Sprite spriteBarracksOutline;
    [SerializeField] private GameObject barracksShadow;

    [SerializeField] private Text needCostTree;
    [SerializeField] private Text needCostStone;
    [SerializeField] private Text needCostCoin;

    [SerializeField] private Text factCountTree;
    [SerializeField] private Text factCountStone;
    [SerializeField] private Text factCountCoin;

    public List<GameObject> barracksList = new List<GameObject>();
    // ----------------------------------

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateConstructionCost();

        if (GameData.countBarracks > 1)
        {
            for (int i = 2; i <= GameData.countBarracks; i++)
            {
                foreach (GameObject barracks in barracksList)
                {
                    if (barracks.tag == "UnbuiltBarracks")
                    {
                        barracks.GetComponent<SpriteRenderer>().sprite = spriteBarracks;
                        OutlineOnObj barracksOutline = barracks.GetComponent<OutlineOnObj>();
                        barracksOutline.mainSprite = spriteBarracks;
                        barracksOutline.outlineSprite = spriteBarracksOutline;

                        Instantiate(barracksShadow, barracks.transform);

                        barracks.transform.GetChild(0).gameObject.SetActive(false);
                        barracks.transform.GetChild(1).gameObject.SetActive(true);
                        barracks.transform.GetChild(2).gameObject.SetActive(true);

                        barracks.tag = $"Barracks_{i}";
                        break;
                    }
                }
            }
        }
    }

    // Поле публичных методов
    public void PanelUnbuiltBarracksOn() // Изменять в зависимсоти от панели здания
    {
        panelUnbuiltBarracks.SetActive(true);
    }

    public void PanelUnbuiltBarrackOff() // Изменять в зависимсоти от панели здания
    {
        panelUnbuiltBarracks.SetActive(false);
    }
    public void BuildBarracks()
    {
        if (CanBuild())
        {
            CreateBarracks();
        }
    }
    // ----------------------------------
    private bool CanBuild()
    {
        return int.Parse(factCountTree.text) >= int.Parse(needCostTree.text) &&
               int.Parse(factCountStone.text) >= int.Parse(needCostStone.text) &&
               int.Parse(factCountCoin.text) >= int.Parse(needCostCoin.text);
    }

    private void UpdateConstructionCost()
    {
        needCostTree.text = buildingBuildCostSO.priceTree1.ToString();
        needCostStone.text = buildingBuildCostSO.priceStone1.ToString();
        needCostCoin.text = buildingBuildCostSO.priceCoin1.ToString();
    }

    private void CreateBarracks()
    {
        foreach (GameObject barracks in barracksList)
        {
            if (barracks.tag == "UnbuiltBarracks")
            {
                barracks.GetComponent<SpriteRenderer>().sprite = spriteBarracks;
                OutlineOnObj barracksOutline = barracks.GetComponent<OutlineOnObj>();
                barracksOutline.mainSprite = spriteBarracks;
                barracksOutline.outlineSprite = spriteBarracksOutline;

                Instantiate(barracksShadow, barracks.transform);

                barracks.transform.GetChild(0).gameObject.SetActive(false);
                barracks.transform.GetChild(1).gameObject.SetActive(true);
                barracks.transform.GetChild(2).gameObject.SetActive(true);

                GameData.countBarracks += 1;
                barracks.tag = $"Barracks_{GameData.countBarracks}";
                GameData.mercenaryList[$"Mercenary_{GameData.countBarracks}"] = true;

                PanelUnbuiltBarrackOff();
                return;
            }

            DeductResources();
        }
    }

    private void DeductResources()
    {
        int differenceTree = int.Parse(factCountTree.text) - int.Parse(needCostTree.text);
        int differenceStone = int.Parse(factCountStone.text) - int.Parse(needCostStone.text);
        int differenceCoin = int.Parse(factCountCoin.text) - int.Parse(needCostCoin.text);

        GameData.resourceCounts["Tree"] = differenceTree;
        GameData.resourceCounts["Stone"] = differenceStone;
        GameData.resourceCounts["Coin"] = differenceCoin;

        ResourcesPanel.Instance.UpdateResourceList();
    }
}
