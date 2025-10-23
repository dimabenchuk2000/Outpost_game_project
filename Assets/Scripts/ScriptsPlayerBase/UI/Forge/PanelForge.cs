public class PanelForge : BaseBuildingPanel
{
    new public static PanelForge Instance;

    // Переопределяем абстрактные свойства
    protected override string buildingKey => "Forge";
    protected override string GetBuildingName => "Кузница";

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
    public void PanelForgeOn()
    {
        ShowPanel();
    }

    public void PanelForgeOff()
    {
        HidePanel();
    }
}
