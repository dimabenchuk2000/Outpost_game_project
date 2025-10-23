using System;
using Outpost.Player_Attack;
using UnityEngine;
using UnityEngine.UI;

public class PanelWeapon : MonoBehaviour
{
    public static PanelWeapon Instance;

    // Поле переменных
    [SerializeField] private Image _wepaonSelection;
    [SerializeField] private Image _pickaxeSelection;
    [SerializeField] private Image _axeSelection;
    // ----------------------------------

    // Поле событий
    public event EventHandler OnEquipWeapon;
    public event EventHandler OnEquipPickaxe;
    public event EventHandler OnEquipAxe;
    // ----------------------------------

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnWeaponSelection += GameInput_OnWeaponSelection;
        GameInput.Instance.OnPickaxeSelection += GameInput_OnPickaxeSelection;
        GameInput.Instance.OnAxeSelection += GameInput_OnAxeSelection;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnWeaponSelection -= GameInput_OnWeaponSelection;
        GameInput.Instance.OnPickaxeSelection -= GameInput_OnPickaxeSelection;
        GameInput.Instance.OnAxeSelection -= GameInput_OnAxeSelection;
    }

    // Поле приватных методов
    private void GameInput_OnWeaponSelection(object sender, EventArgs e)
    {
        if (Player_Attack.IsPlayerFightMode() == false && !Player.Instance.isPlayerTPBase)
        {
            _wepaonSelection.color = new Color32(156, 98, 42, 255);
            _pickaxeSelection.color = new Color32(132, 85, 40, 255);
            _axeSelection.color = new Color32(132, 85, 40, 255);

            OnEquipWeapon?.Invoke(this, EventArgs.Empty); // ---> ActiveWeapon
        }
    }

    private void GameInput_OnPickaxeSelection(object sender, EventArgs e)
    {
        if (Player_Attack.IsPlayerFightMode() == false && !Player.Instance.isPlayerTPBase)
        {
            _pickaxeSelection.color = new Color32(156, 98, 42, 255);
            _wepaonSelection.color = new Color32(132, 85, 40, 255);
            _axeSelection.color = new Color32(132, 85, 40, 255);

            OnEquipPickaxe?.Invoke(this, EventArgs.Empty); // ---> ActiveWeapon
        }
    }

    private void GameInput_OnAxeSelection(object sender, EventArgs e)
    {
        if (Player_Attack.IsPlayerFightMode() == false && !Player.Instance.isPlayerTPBase)
        {
            _axeSelection.color = new Color32(156, 98, 42, 255);
            _pickaxeSelection.color = new Color32(132, 85, 40, 255);
            _wepaonSelection.color = new Color32(132, 85, 40, 255);

            OnEquipAxe?.Invoke(this, EventArgs.Empty); // ---> ActiveWeapon
        }

    }
    // ----------------------------------
}
