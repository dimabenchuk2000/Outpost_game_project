using System;
using Outpost.Player_Movement;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance;

    // Поле переменных
    [SerializeField] private DataBase _dataBase;
    // ----------------------------------

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PanelWeapon.Instance.OnEquipWeapon += PanelWeapon_OnEquipWeapon;
        PanelWeapon.Instance.OnEquipPickaxe += PanelWeapon_OnEquipPickaxe;
        PanelWeapon.Instance.OnEquipAxe += PanelWeapon_OnEquipAxe;
    }

    private void OnDestroy()
    {
        PanelWeapon.Instance.OnEquipWeapon -= PanelWeapon_OnEquipWeapon;
        PanelWeapon.Instance.OnEquipPickaxe -= PanelWeapon_OnEquipPickaxe;
        PanelWeapon.Instance.OnEquipAxe -= PanelWeapon_OnEquipAxe;
    }

    private void Update()
    {
        WeaponRotation();
    }
    // Поле публичных методов
    public GameObject CheckActiveWeapon()
    {
        GameObject activeWeapon = gameObject.transform.GetChild(0).gameObject;
        return activeWeapon;
    }
    // ----------------------------------

    // Поле приватных методов
    private void WeaponRotation()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player_Movement.GetPlayerPosition();

        if (mousePos.x < playerPos.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void DestroyNotActiveWeapon()
    {
        if (this.transform.childCount != 0)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
    }

    private void PanelWeapon_OnEquipWeapon(object sender, EventArgs e)
    {
        DestroyNotActiveWeapon();

        GameObject newWeapon = Instantiate(_dataBase._items[3].obj);
        newWeapon.transform.SetParent(this.transform);
        newWeapon.transform.localPosition = new Vector3(-0.354f, 1.415f, 0);

        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player_Movement.GetPlayerPosition();

        if (mousePos.x < playerPos.x)
            newWeapon.transform.localRotation = Quaternion.Euler(0, 0, -192);
    }

    private void PanelWeapon_OnEquipPickaxe(object sender, EventArgs e)
    {
        DestroyNotActiveWeapon();

        GameObject newWeapon = Instantiate(_dataBase._items[4].obj);
        newWeapon.transform.SetParent(this.transform);
        newWeapon.transform.localPosition = new Vector3(-0.022f, 0.73f, 0);

        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player_Movement.GetPlayerPosition();

        if (mousePos.x < playerPos.x)
            newWeapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void PanelWeapon_OnEquipAxe(object sender, EventArgs e)
    {
        DestroyNotActiveWeapon();

        GameObject newWeapon = Instantiate(_dataBase._items[5].obj);
        newWeapon.transform.SetParent(this.transform);
        newWeapon.transform.localPosition = new Vector3(-0.022f, 0.73f, 0);

        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player_Movement.GetPlayerPosition();

        if (mousePos.x < playerPos.x)
            newWeapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    // ----------------------------------
}
