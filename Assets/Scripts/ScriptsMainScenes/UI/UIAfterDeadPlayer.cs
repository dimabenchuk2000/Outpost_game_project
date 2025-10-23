using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIAfterDeadPlayer : MonoBehaviour
{
    public static UIAfterDeadPlayer Instance;

    [SerializeField] private Text textYouDead;
    [SerializeField] private Text textButNotEnd;
    [SerializeField] private float fadeDuration;

    private void Awake()
    {
        Instance = this;
    }

    // Поле публичных методов
    public void LightenTextYouDead()
    {
        StartCoroutine(FadeTo(1f, textYouDead));
    }

    public void LightenTextButNotEnd()
    {
        StartCoroutine(FadeTo(1f, textButNotEnd));
    }
    // ----------------------------------

    // Поле корутин
    private IEnumerator FadeTo(float targetAlpha, Text text)
    {
        float startAlpha = text.color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            text.color = new Color(255, 255, 255, newAlpha);
            yield return null;
        }

        text.color = new Color(255, 255, 255, targetAlpha);
    }
    // ----------------------------------
}
