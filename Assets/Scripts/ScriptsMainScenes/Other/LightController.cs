using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]

public class LightController : MonoBehaviour
{
    public static LightController Instanse;

    // Поле переменных
    public Light2D _light2D;
    public bool _isPlayerInCave = false;
    // ----------------------------------

    private void Awake()
    {
        Instanse = this;
        _light2D = GetComponent<Light2D>();
    }

    // Поле публичных методов
    public void CheckingPlayerLocation()
    {
        if (!_isPlayerInCave)
            _light2D.intensity = 1;

        if (_isPlayerInCave)
            _light2D.intensity = .6f;
    }
    // ----------------------------------
}
