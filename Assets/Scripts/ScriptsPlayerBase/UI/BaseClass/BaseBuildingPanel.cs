using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBuildingPanel : MonoBehaviour
{
    public static BaseBuildingPanel Instance;

    [SerializeField] protected BuildingUpgradeCostSO buildingUpgradeCostSO;

    [SerializeField] protected GameObject panel;
    [SerializeField] protected Text textLevel;
    [SerializeField] protected GameObject textCostUpgrade;
    [SerializeField] protected Text textButtonUpgrade;

    [SerializeField] protected Text needCostTree;
    [SerializeField] protected Text needCostStone;
    [SerializeField] protected Text needCostCoin;

    [SerializeField] protected Text factCountTree;
    [SerializeField] protected Text factCountStone;
    [SerializeField] protected Text factCountCoin;

    protected const int MAX_LEVEL = 5;
    public int currentLevel;

    protected abstract string buildingKey { get; } // Ключ для хранения уровня здания
    protected abstract string GetBuildingName { get; } // Имя здания для отображения

    protected virtual void Awake()
    {
        Instance = this;
    }

    protected virtual void Start()
    {
        currentLevel = GameData.buildingsLevel[buildingKey];
        UpdateBuildingLevel();
    }

    // Публичные методы
    public virtual void UpgradeBuilding()
    {
        if (currentLevel >= MAX_LEVEL)
            return;

        if (currentLevel < GetRequiredTownHallLevel())
        {
            if (CanUpgrade())
            {
                DeductResources();
                currentLevel++;
                UpdateBuildingLevel();
            }
        }
        else
        {
            Debug.Log($"Требуется улучшить ратушу");
        }
    }

    // Защищенные методы
    protected void ShowPanel()
    {
        panel.SetActive(true);
    }

    protected void HidePanel()
    {
        panel.SetActive(false);
    }

    protected bool CanUpgrade()
    {
        return int.Parse(factCountTree.text) >= GetRequiredResource("Tree") &&
               int.Parse(factCountStone.text) >= GetRequiredResource("Stone") &&
               int.Parse(factCountCoin.text) >= GetRequiredResource("Coin");
    }

    protected int GetRequiredResource(string resourceType)
    {
        switch (currentLevel)
        {
            case 1:
                return resourceType == "Tree" ? buildingUpgradeCostSO.priceTree1 :
                       resourceType == "Stone" ? buildingUpgradeCostSO.priceStone1 :
                       buildingUpgradeCostSO.priceCoin1;
            case 2:
                return resourceType == "Tree" ? buildingUpgradeCostSO.priceTree2 :
                       resourceType == "Stone" ? buildingUpgradeCostSO.priceStone2 :
                       buildingUpgradeCostSO.priceCoin2;
            case 3:
                return resourceType == "Tree" ? buildingUpgradeCostSO.priceTree3 :
                       resourceType == "Stone" ? buildingUpgradeCostSO.priceStone3 :
                       buildingUpgradeCostSO.priceCoin3;
            case 4:
                return resourceType == "Tree" ? buildingUpgradeCostSO.priceTree4 :
                       resourceType == "Stone" ? buildingUpgradeCostSO.priceStone4 :
                       buildingUpgradeCostSO.priceCoin4;
            default:
                return 0;
        }
    }

    protected void UpdateBuildingLevel()
    {
        textLevel.text = $"{GetBuildingName} - {currentLevel} ур.";
        needCostTree.text = GetRequiredResource("Tree").ToString();
        needCostStone.text = GetRequiredResource("Stone").ToString();
        needCostCoin.text = GetRequiredResource("Coin").ToString();
        GameData.buildingsLevel[buildingKey] = currentLevel;

        if (currentLevel >= MAX_LEVEL)
        {
            textCostUpgrade.SetActive(false);
            textButtonUpgrade.text = "Макс. Уровень";
        }
    }


    protected void DeductResources()
    {
        int differenceTree = int.Parse(factCountTree.text) - GetRequiredResource("Tree");
        int differenceStone = int.Parse(factCountStone.text) - GetRequiredResource("Stone");
        int differenceCoin = int.Parse(factCountCoin.text) - GetRequiredResource("Coin");

        GameData.resourceCounts["Tree"] = differenceTree;
        GameData.resourceCounts["Stone"] = differenceStone;
        GameData.resourceCounts["Coin"] = differenceCoin;

        ResourcesPanel.Instance.UpdateResourceList();
    }

    // Дополнительные методы
    protected int GetRequiredTownHallLevel()
    {
        return PanelTownHall.Instance.currentLevel;
    }
}
