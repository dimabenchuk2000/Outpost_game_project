public class PanelBarracks_5 : BaseBuildingPanel
{
    new public static PanelBarracks_5 Instance;

    // Переопределяем абстрактные свойства
    protected override string buildingKey => "Barracks_5";
    protected override string GetBuildingName => "Казарма";

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
    public void PanelBarracksOn()
    {
        ShowPanel();
    }

    public void PanelBarracksOff()
    {
        HidePanel();
    }
}
