using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelPortal : MonoBehaviour
{
    public static PanelPortal Instance;

    // Поле переменных
    [SerializeField] private GameObject panelPortal;
    // ----------------------------------

    private void Awake()
    {
        Instance = this;
    }

    // Поле публичных методов
    public void PanelPortalOn()
    {
        panelPortal.SetActive(true);
    }

    public void PanelPortalOff()
    {
        panelPortal.SetActive(false);
    }

    public void GoToMap()
    {
        SceneManager.LoadScene("Map01", LoadSceneMode.Single);
    }
    // ----------------------------------
}
