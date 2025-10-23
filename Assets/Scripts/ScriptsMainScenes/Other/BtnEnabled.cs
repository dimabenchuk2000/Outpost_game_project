using UnityEngine;

public class BtnEnabled : MonoBehaviour
{
    public static BtnEnabled Instance;

    [SerializeField] private GameObject buttonGoToBase;

    private void Awake()
    {
        Instance = this;
    }

    public void ButtonGoToBaseOn()
    {
        if (buttonGoToBase != null)
            buttonGoToBase.SetActive(true);
    }
    public void ButtonGoToBaseOff()
    {
        if (buttonGoToBase != null)
            buttonGoToBase.SetActive(false);
    }
}
