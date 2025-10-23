using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    // Поле переменных
    [SerializeField] private GameObject textInventoryFull;
    [SerializeField] private RectTransform inventory;
    [SerializeField] private RectTransform inventoryBG;

    public DataBase _data;
    public Camera _camera;

    public List<ItemInventory> _items = new List<ItemInventory>();

    public GameObject _gameObjShow;
    public GameObject _inventoryMainObj;
    public int _maxCount;

    public GameObject _backGround;
    // ----------------------------------
    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        if (GameData.improvementList["Inventory_12_slots"] && !GameData.improvementList["Inventory_15_slots"])
        {
            inventory.sizeDelta = new Vector2(210, inventory.sizeDelta.y);
            inventoryBG.sizeDelta = new Vector2(235, inventoryBG.sizeDelta.y);
            _maxCount = 12;
        }
        else if (GameData.improvementList["Inventory_15_slots"])
        {
            inventory.sizeDelta = new Vector2(260, inventory.sizeDelta.y);
            inventoryBG.sizeDelta = new Vector2(285, inventoryBG.sizeDelta.y);
            _maxCount = 15;
        }


        if (_items.Count == 0)
        {
            AddGraphics();
        }

        GameInput.Instance.OnInventoryToggle += GameInput_OnInventoryToggle;
    }

    public void OnDestroy()
    {
        GameInput.Instance.OnInventoryToggle -= GameInput_OnInventoryToggle;
    }

    // Поле методов
    public void AddItem(Item item, int count, string tag)
    {
        for (int i = 0; i < _maxCount; i++)
        {
            if (item.obj.tag == "Coin")
            {
                if (_items[i].itemGameObj.tag == "Empty" || _items[i].itemGameObj.tag == "Coin")
                {
                    _items[i].id = item.id;
                    _items[i].count += count;
                    _items[i].itemGameObj.GetComponent<Image>().sprite = item.img;

                    if (count > 1 && item.id != 0)
                    {
                        _items[i].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();
                    }
                    else
                    {
                        _items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
                    }

                    _items[i].itemGameObj.tag = tag;

                    break;
                }
            }
            if (_items[i].itemGameObj.tag == "Empty")
            {
                _items[i].id = item.id;
                _items[i].count = count;
                _items[i].itemGameObj.GetComponent<Image>().sprite = item.img;

                if (count > 1 && item.id != 0)
                {
                    _items[i].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();
                }
                else
                {
                    _items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
                }

                _items[i].itemGameObj.tag = tag;

                break;
            }
        }

    }

    public void RemoveItem(int i)
    {
        _items[i].id = 0;
        _items[i].count = 0;
        _items[i].itemGameObj.GetComponent<Image>().sprite = DataBase.Instance._items[0].img;
        _items[i].itemGameObj.tag = "Empty";
    }

    public void AddGraphics()
    {
        for (int i = 0; i < _maxCount; i++)
        {
            GameObject newItem = Instantiate(_gameObjShow, _inventoryMainObj.transform);

            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            _items.Add(ii);
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < _maxCount; i++)
        {
            if (_items[i].id != 0 && _items[i].count > 1)
            {
                _items[i].itemGameObj.GetComponentInChildren<Text>().text = _items[i].count.ToString();
            }
            else
            {
                _items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
            }

            _items[i].itemGameObj.GetComponent<Image>().sprite = _data._items[_items[i].id].img;
        }
    }

    public void AddTextInventoryFull()
    {
        textInventoryFull.SetActive(true);
        StartCoroutine(InventoryFullStop());
    }
    // ----------------------------------

    // Поле приватных методов
    private void GameInput_OnInventoryToggle(object sender, System.EventArgs e)
    {
        _backGround.SetActive(!_backGround.activeSelf);
        if (_backGround.activeSelf)
        {
            UpdateInventory();
        }
    }
    // ----------------------------------

    // Поле корутин
    IEnumerator InventoryFullStop()
    {
        yield return new WaitForSeconds(1);
        textInventoryFull.SetActive(false);
    }
    // ----------------------------------
}

[System.Serializable]

public class ItemInventory
{
    public int id;
    public GameObject itemGameObj;
    public int count;
}
