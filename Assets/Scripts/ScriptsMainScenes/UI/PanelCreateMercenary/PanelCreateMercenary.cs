using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCreateMercenary : MonoBehaviour
{
    public static PanelCreateMercenary Instance;

    [SerializeField] private Text textCountMercenary;
    [SerializeField] private GameObject mercenaryTemplate;

    public bool isCreateTemplate = false;
    public List<GameObject> allies;

    private int _countMercenary;
    private int _mercenarySwordLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _countMercenary = CheckingCountMercenary.Instance.currentCount;
    }

    public int MercenarySwordLevel() => _mercenarySwordLevel;

    public void CreateMercenary()
    {
        if (_countMercenary > 0 && !isCreateTemplate)
        {
            if (allies.Count == 0)
                _mercenarySwordLevel = GameData.weaponLevel["Mercenary_1_SwordLevel"];

            if (allies.Count == 1)
                _mercenarySwordLevel = GameData.weaponLevel["Mercenary_2_SwordLevel"];

            if (allies.Count == 2)
                _mercenarySwordLevel = GameData.weaponLevel["Mercenary_3_SwordLevel"];

            if (allies.Count == 3)
                _mercenarySwordLevel = GameData.weaponLevel["Mercenary_4_SwordLevel"];

            if (allies.Count == 4)
                _mercenarySwordLevel = GameData.weaponLevel["Mercenary_5_SwordLevel"];

            if (allies.Count == 5)
                _mercenarySwordLevel = GameData.weaponLevel["Mercenary_6_SwordLevel"];

            isCreateTemplate = true;
            Instantiate(mercenaryTemplate);
            _countMercenary -= 1;
            textCountMercenary.text = _countMercenary.ToString();
        }
    }
}
