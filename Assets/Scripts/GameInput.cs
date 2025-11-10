using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    // Поле переменных
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference fightModeSwitch;
    [SerializeField] private InputActionReference attackTop;
    [SerializeField] private InputActionReference attackDown;
    [SerializeField] private InputActionReference inventory;
    [SerializeField] private InputActionReference weaponSelection;
    [SerializeField] private InputActionReference pickaxeSelection;
    [SerializeField] private InputActionReference axeSelection;
    [SerializeField] private InputActionReference cameraPortal;
    [SerializeField] private InputActionReference dash;
    [SerializeField] private InputActionReference run;
    // ----------------------------------

    // Поле событий
    public event EventHandler OnPlayerFightMode;
    public event EventHandler OnPlayerAttackTop;
    public event EventHandler OnPlayerAttackDown;
    public event EventHandler OnInventoryToggle;
    public event EventHandler OnWeaponSelection;
    public event EventHandler OnPickaxeSelection;
    public event EventHandler OnAxeSelection;
    public event EventHandler OnCameraPortalToggle;
    public event EventHandler OnPlayerDashStarted;
    public event EventHandler OnPlayerTPBase;
    public event EventHandler OnPlayerRunPerformed;
    public event EventHandler OnPlayerRunCancaled;
    // ----------------------------------

    private void Awake()
    {
        Instance = this;
        if (SceneManager.GetActiveScene().name == "Map01")
        {
            fightModeSwitch.action.started += PlayerFightMode;
            attackTop.action.started += PlayerAttackTop;
            attackDown.action.started += PlayerAttackDown;
            inventory.action.started += InventoryToggle;
            weaponSelection.action.started += WeaponSelection;
            pickaxeSelection.action.started += PickaxeSelection;
            axeSelection.action.started += AxeSelection;
            cameraPortal.action.started += CameraPortalToggle;

            if (GameData.improvementList["Dash"])
                dash.action.started += PlayerDashStarted;

            if (GameData.improvementList["Run"])
            {
                run.action.performed += PlayerRunPerformed;
                run.action.canceled += PlayerRunCanceled;
            }
        }
    }

    // Поле публичных методов
    public Vector2 GetVectorDirectionMovement()
    {
        Vector2 vectorDirectionMovement = move.action.ReadValue<Vector2>();
        return vectorDirectionMovement;
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        return mousePosition;
    }

    public Vector3 GetVectorDirectionToMouse()
    {
        Vector3 vectorDirectionToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vectorDirectionToMouse.z = 0;
        return vectorDirectionToMouse;
    }

    public void GoToBase()
    {
        Player.Instance.isPlayerTPBase = true;
        OnPlayerTPBase?.Invoke(this, EventArgs.Empty); // ---> PlayerVisual
    }
    // ----------------------------------

    // Поле приватных методов
    private void PlayerFightMode(InputAction.CallbackContext context)
    {
        OnPlayerFightMode?.Invoke(this, EventArgs.Empty); // ---> Player
    }

    private void PlayerAttackTop(InputAction.CallbackContext context)
    {
        OnPlayerAttackTop?.Invoke(this, EventArgs.Empty); // ---> Player
    }

    private void PlayerAttackDown(InputAction.CallbackContext context)
    {
        OnPlayerAttackDown?.Invoke(this, EventArgs.Empty); // ---> Player
    }

    private void InventoryToggle(InputAction.CallbackContext context)
    {
        OnInventoryToggle?.Invoke(this, EventArgs.Empty); // ---> Inventory
    }

    private void WeaponSelection(InputAction.CallbackContext context)
    {
        OnWeaponSelection?.Invoke(this, EventArgs.Empty); // ---> PanelWeapon
    }

    private void PickaxeSelection(InputAction.CallbackContext context)
    {
        OnPickaxeSelection?.Invoke(this, EventArgs.Empty); // ---> PanelWeapon
    }

    private void AxeSelection(InputAction.CallbackContext context)
    {
        OnAxeSelection?.Invoke(this, EventArgs.Empty); // ---> PanelWeapon
    }

    private void CameraPortalToggle(InputAction.CallbackContext context)
    {
        OnCameraPortalToggle?.Invoke(this, EventArgs.Empty); // ---> CameraPortal
    }

    private void PlayerDashStarted(InputAction.CallbackContext context)
    {
        OnPlayerDashStarted?.Invoke(this, EventArgs.Empty); // ---> Player
    }

    private void PlayerRunPerformed(InputAction.CallbackContext context)
    {
        OnPlayerRunPerformed?.Invoke(this, EventArgs.Empty); // ---> Player
    }

    private void PlayerRunCanceled(InputAction.CallbackContext context)
    {
        OnPlayerRunCancaled?.Invoke(this, EventArgs.Empty); // ---> Player
    }
    // ----------------------------------
}
