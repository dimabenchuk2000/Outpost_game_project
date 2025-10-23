public class PanelTownHall : BaseBuildingPanel
{
    new public static PanelTownHall Instance;

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
        }
    }
}
