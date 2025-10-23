using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraPortal : MonoBehaviour
{
    public static CameraPortal Instance;
    // Поле переменных
    private Camera _cameraPortal;
    // ----------------------------------

    private void Awake()
    {
        Instance = this;
        _cameraPortal = GetComponent<Camera>();
    }

    private void Start()
    {
        GameInput.Instance.OnCameraPortalToggle += GameInput_OnCameraPortalToggle;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnCameraPortalToggle -= GameInput_OnCameraPortalToggle;
    }

    // Поле публичных методов
    public void CameraPortalToggle()
    {
        if (!PortalPlayer.Instance.isPortalDestruction)
        {
            _cameraPortal.enabled = !_cameraPortal.enabled;

            if (_cameraPortal.enabled)
            {
                LightController.Instanse._light2D.intensity = 1;
            }

            if (!_cameraPortal.enabled)
            {
                LightController.Instanse.CheckingPlayerLocation();
            }
        }

    }
    // ----------------------------------

    // Поле приватных методов
    private void GameInput_OnCameraPortalToggle(object sender, EventArgs e)
    {
        CameraPortalToggle();
    }
    // ----------------------------------
}
