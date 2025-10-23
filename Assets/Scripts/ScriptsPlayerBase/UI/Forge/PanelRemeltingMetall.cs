using UnityEngine;

public class PanelRemeltingMetall : MonoBehaviour
{
    [SerializeField] private GameObject panelRemeltingCopper;
    [SerializeField] private GameObject panelRemeltingIron;
    [SerializeField] private GameObject panelRemeltingTitan;
    [SerializeField] private GameObject panelRemeltingDiamond;

    public void panelRemeltingCopperOn()
    {
        panelRemeltingCopper.SetActive(true);
        panelRemeltingIron.SetActive(false);
        panelRemeltingTitan.SetActive(false);
        panelRemeltingDiamond.SetActive(false);
    }

    public void panelRemeltingIronOn()
    {
        panelRemeltingCopper.SetActive(false);
        panelRemeltingIron.SetActive(true);
        panelRemeltingTitan.SetActive(false);
        panelRemeltingDiamond.SetActive(false);
    }

    public void panelRemeltingTitanOn()
    {
        panelRemeltingCopper.SetActive(false);
        panelRemeltingIron.SetActive(false);
        panelRemeltingTitan.SetActive(true);
        panelRemeltingDiamond.SetActive(false);
    }

    public void panelRemeltingDiamondOn()
    {
        panelRemeltingCopper.SetActive(false);
        panelRemeltingIron.SetActive(false);
        panelRemeltingTitan.SetActive(false);
        panelRemeltingDiamond.SetActive(true);
    }
}
