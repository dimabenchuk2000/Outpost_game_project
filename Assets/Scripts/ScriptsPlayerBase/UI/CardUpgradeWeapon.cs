using UnityEngine;
using UnityEngine.UI;

public class CardUpgradeWeapon : MonoBehaviour
{
    [SerializeField] private WeaponUpgradeCostSO weaponUpgradeCostSO;
    [SerializeField] private Sprite[] spriteWeapon;
    [SerializeField] private Sprite[] spriteMetall;

    [SerializeField] private Image imageWeapon;
    [SerializeField] private Image imageCostMetall;

    [SerializeField] private string nameWeapon;
    [SerializeField] private GameObject textCostUpgradeWeapon;
    [SerializeField] private Text textButtonUpgradeWeapon;

    [SerializeField] private Text needCostMetall;
    [SerializeField] private Text needCostCoin;

    [SerializeField] private Text factCountCoin;

    private const int MAX_LEVEL = 5;
    private int _currentLevel;
    private int factCountMetall;

    private void Awake()
    {
        _currentLevel = GameData.weaponLevel[nameWeapon];
    }

    private void Start()
    {
        UpdateBuildingLevel();
    }
    // Публичные метода --------------------------------------
    public void UpgradeWeaponForge()
    {
        if (_currentLevel >= MAX_LEVEL)
        {
            return;
        }
        if (_currentLevel < PanelForge.Instance.currentLevel)
        {
            if (CanUpgrade())
            {
                DeductResources();
                _currentLevel++;
                UpdateBuildingLevel();
            }
        }
        else
        {
            Debug.Log("Требуется улучшить кузницу");
        }
    }

    public void UpgradeWeaponBarracks_1()
    {
        if (_currentLevel >= MAX_LEVEL)
        {
            return;
        }
        if (_currentLevel < PanelBarracks.Instance.currentLevel)
        {
            if (CanUpgrade())
            {
                DeductResources();
                _currentLevel++;
                UpdateBuildingLevel();
            }
        }
        else
        {
            Debug.Log("Требуется улучшить казарму");
        }
    }

    public void UpgradeWeaponBarracks_2()
    {
        if (_currentLevel >= MAX_LEVEL)
        {
            return;
        }
        if (_currentLevel < PanelBarracks_2.Instance.currentLevel)
        {
            if (CanUpgrade())
            {
                DeductResources();
                _currentLevel++;
                UpdateBuildingLevel();
            }
        }
        else
        {
            Debug.Log("Требуется улучшить казарму");
        }
    }

    public void UpgradeWeaponBarracks_3()
    {
        if (_currentLevel >= MAX_LEVEL)
        {
            return;
        }
        if (_currentLevel < PanelBarracks_3.Instance.currentLevel)
        {
            if (CanUpgrade())
            {
                DeductResources();
                _currentLevel++;
                UpdateBuildingLevel();
            }
        }
        else
        {
            Debug.Log("Требуется улучшить казарму");
        }
    }

    public void UpgradeWeaponBarracks_4()
    {
        if (_currentLevel >= MAX_LEVEL)
        {
            return;
        }
        if (_currentLevel < PanelBarracks_4.Instance.currentLevel)
        {
            if (CanUpgrade())
            {
                DeductResources();
                _currentLevel++;
                UpdateBuildingLevel();
            }
        }
        else
        {
            Debug.Log("Требуется улучшить казарму");
        }
    }

    public void UpgradeWeaponBarracks_5()
    {
        if (_currentLevel >= MAX_LEVEL)
        {
            return;
        }
        if (_currentLevel < PanelBarracks_5.Instance.currentLevel)
        {
            if (CanUpgrade())
            {
                DeductResources();
                _currentLevel++;
                UpdateBuildingLevel();
            }
        }
        else
        {
            Debug.Log("Требуется улучшить казарму");
        }
    }

    public void UpgradeWeaponBarracks_6()
    {
        if (_currentLevel >= MAX_LEVEL)
        {
            return;
        }
        if (_currentLevel < PanelBarracks_6.Instance.currentLevel)
        {
            if (CanUpgrade())
            {
                DeductResources();
                _currentLevel++;
                UpdateBuildingLevel();
            }
        }
        else
        {
            Debug.Log("Требуется улучшить казарму");
        }
    }
    // ---------------------------------------------------------------

    private bool CanUpgrade()
    {
        return factCountMetall >= int.Parse(needCostMetall.text) &&
               int.Parse(factCountCoin.text) >= GetRequiredResource("Coin");
    }

    private int GetRequiredResource(string resourceType)
    {
        switch (_currentLevel)
        {
            case 1:
                return resourceType == "Coin" ? weaponUpgradeCostSO.priceCoin1 : GameData.resourceCounts["Copper"];
            case 2:
                return resourceType == "Coin" ? weaponUpgradeCostSO.priceCoin2 : GameData.resourceCounts["Iron"];
            case 3:
                return resourceType == "Coin" ? weaponUpgradeCostSO.priceCoin3 : GameData.resourceCounts["Titan"];
            case 4:
                return resourceType == "Coin" ? weaponUpgradeCostSO.priceCoin4 : GameData.resourceCounts["Diamond"];
            default:
                return 0;
        }
    }

    private void UpdateBuildingLevel()
    {
        needCostMetall.text = weaponUpgradeCostSO.priceMetall.ToString();
        needCostCoin.text = GetRequiredResource("Coin").ToString();
        factCountMetall = GetRequiredResource("Metall");
        GameData.weaponLevel[nameWeapon] = _currentLevel;
        imageWeapon.sprite = spriteWeapon[_currentLevel - 1];

        if (_currentLevel <= 4)
            imageCostMetall.sprite = spriteMetall[_currentLevel - 1];

        if (_currentLevel >= MAX_LEVEL)
        {
            textCostUpgradeWeapon.SetActive(false);
            textButtonUpgradeWeapon.text = "Макс. Уровень";
        }
    }

    private void DeductResources()
    {
        int differenceMetall = factCountMetall - int.Parse(needCostMetall.text);
        int differenceCoin = int.Parse(factCountCoin.text) - GetRequiredResource("Coin");

        factCountMetall = differenceMetall;
        factCountCoin.text = differenceCoin.ToString();

        GameData.resourceCounts["Coin"] = differenceCoin;

        switch (_currentLevel)
        {
            case 1:
                GameData.resourceCounts["Copper"] = differenceMetall;
                break;
            case 2:
                GameData.resourceCounts["Iron"] = differenceMetall;
                break;
            case 3:
                GameData.resourceCounts["Titan"] = differenceMetall;
                break;
            case 4:
                GameData.resourceCounts["Diamond"] = differenceMetall;
                break;
            default:
                break;
        }

        ResourcesPanel.Instance.UpdateResourceList();
    }
}
