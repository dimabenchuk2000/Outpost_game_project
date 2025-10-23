using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgradeCostSO", menuName = "Scriptable Objects/WeaponUpgradeCostSO")]
public class WeaponUpgradeCostSO : ScriptableObject
{
    public string nameWeapon;
    public string level2;
    public int priceMetall;
    public int priceCoin1;

    public string level3;
    public int priceCoin2;

    public string level4;
    public int priceCoin3;

    public string level5;
    public int priceCoin4;
}
