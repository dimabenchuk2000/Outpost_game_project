using UnityEngine;
using UnityEngine.UI;

public class PanelTownHall : BaseBuildingPanel
{
    new public static PanelTownHall Instance;

    [SerializeField] private Text _textImprovements;

    // Переопределяем абстрактные свойства
    protected override string buildingKey => "TownHall";
    protected override string GetBuildingName => "Ратуша";

    new private void Awake()
    {
        Instance = this;
        base.Awake();
    }

    new private void Start()
    {
        base.Start();
        ChangeTextImprovement();
    }

    // Публичные методы для управления видимостью панели
    public void PanelTownHallOn()
    {
        ShowPanel();
    }

    public void PanelTownHallOff()
    {
        HidePanel();
    }

    public override void UpgradeBuilding()
    {
        if (currentLevel >= MAX_LEVEL)
            return;
        if (CanUpgrade())
        {
            DeductResources();
            currentLevel++;
            UpdateBuildingLevel();
            ChangeTextImprovement();
        }
    }

    // Приватные методы
    private void ChangeTextImprovement()
    {
        switch (currentLevel)
        {
            case 2:
                GameData.improvementList["Dash"] = true;
                _textImprovements.text = "После улучшения здания до 3 ур. расширяется инвентарь на 3 слота";
                break;
            case 3:
                GameData.improvementList["Inventory_12_slots"] = true;
                _textImprovements.text = "После улучшения здания до 4 ур. поялвяется доступ к бегу (shift)";
                break;
            case 4:
                GameData.improvementList["Run"] = true;
                _textImprovements.text = "После улучшения здания до 5 ур. расширяется инвентарь на 3 слота";
                break;
            case 5:
                GameData.improvementList["Inventory_15_slots"] = true;
                _textImprovements.text = "";
                break;
        }
    }
}
