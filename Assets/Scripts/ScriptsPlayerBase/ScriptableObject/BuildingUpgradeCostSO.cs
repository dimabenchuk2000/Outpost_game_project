using UnityEngine;

[CreateAssetMenu(fileName = "BuildingUpgradeCostSO", menuName = "Scriptable Objects/BuildingUpgradeCostSO")]
public class BuildingUpgradeCostSO : ScriptableObject
{
    public string buildName;
    public string level2;
    public int priceTree1;
    public int priceStone1;
    public int priceCoin1;

    public string level3;
    public int priceTree2;
    public int priceStone2;
    public int priceCoin2;

    public string level4;
    public int priceTree3;
    public int priceStone3;
    public int priceCoin3;

    public string level5;
    public int priceTree4;
    public int priceStone4;
    public int priceCoin4;
}
