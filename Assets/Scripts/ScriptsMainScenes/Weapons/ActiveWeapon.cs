using System;
using Outpost.Player_Movement;
using Outpost.WeaponSettings;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance;

    // Поле переменных
    [SerializeField] private DataBase _dataBase;

    private DirectionalRotator _rotator;
    // ----------------------------------

    private void Awake()
    {
        Instance = this;
        _rotator = new DirectionalRotator(null, transform);
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
        _rotator.SetCharacterPos(Camera.main.WorldToScreenPoint(Player.Instance.transform.position));
        _rotator.SetTargetPos(GameInput.Instance.GetMousePosition());
        _rotator.Update(false);
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

    // Создаем общий метод для экипировки оружия
    private void EquipWeapon(string weaponType)
    {
        DestroyNotActiveWeapon();

        if (!WeaponSettings.weaponSettings.TryGetValue(weaponType, out var settings))
        {
            Debug.LogError($"Не найдено оружие типа {weaponType}");
            return;
        }

        GameObject newWeapon = Instantiate(_dataBase._items[settings.ItemIndex].obj);
        newWeapon.transform.SetParent(transform);
        newWeapon.transform.localPosition = settings.LocalPosition;

        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player_Movement.GetPlayerPosition();

        if (mousePos.x < playerPos.x)
            newWeapon.transform.localRotation = Quaternion.Euler(0, 0, settings.RotationAngle);
    }

    // Обновляем обработчики событий
    private void PanelWeapon_OnEquipWeapon(object sender, EventArgs e)
    {
        EquipWeapon("Sword");
    }

    private void PanelWeapon_OnEquipPickaxe(object sender, EventArgs e)
    {
        EquipWeapon("Pickaxe");
    }

    private void PanelWeapon_OnEquipAxe(object sender, EventArgs e)
    {
        EquipWeapon("Axe");
    }
    // ----------------------------------
}
