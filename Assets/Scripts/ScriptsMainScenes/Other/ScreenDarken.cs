using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDarken : MonoBehaviour
{
    public static ScreenDarken Instanse;

    // Поле переменных
    [SerializeField] private float _fadeDuration = 1f;

    private Image _darkOverlay;
    // ----------------------------------

    private void Awake()
    {
        Instanse = this;
        _darkOverlay = GetComponent<Image>();
    }

    // Поле публичных методов
    public void DarkenScreen()
    {
        StartCoroutine(FadeTo(1f));
    }

    public void LightenScreen()
    {
        StartCoroutine(FadeTo(0f));
    }
    // ----------------------------------

    // Поле корутин
    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = _darkOverlay.color.a;
        float elapsed = 0f;

        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / _fadeDuration);
            _darkOverlay.color = new Color(0, 0, 0, newAlpha);
            yield return null;
        }

        _darkOverlay.color = new Color(0, 0, 0, targetAlpha);
    }
    // ----------------------------------
}
