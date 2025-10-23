using UnityEngine;

public class PanelUpgradeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject panelUpgradeSword;
    [SerializeField] private GameObject panelUpgradePickaxe;
    [SerializeField] private GameObject panelUpgradeAxe;

    public void panelUpgradeSwordOn()
    {
        panelUpgradeSword.SetActive(true);
        panelUpgradePickaxe.SetActive(false);
        panelUpgradeAxe.SetActive(false);
    }

    public void panelUpgradePickaxeOn()
    {
        panelUpgradeSword.SetActive(false);
        panelUpgradePickaxe.SetActive(true);
        panelUpgradeAxe.SetActive(false);
    }

    public void panelUpgradeAxeOn()
    {
        panelUpgradeSword.SetActive(false);
        panelUpgradePickaxe.SetActive(false);
        panelUpgradeAxe.SetActive(true);
    }
}
