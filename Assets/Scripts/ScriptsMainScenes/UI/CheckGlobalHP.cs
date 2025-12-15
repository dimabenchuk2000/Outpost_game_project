using UnityEngine;
using UnityEngine.UI;

public class CheckGlobalHP : MonoBehaviour
{
    [SerializeField] private Image _HP_1;
    [SerializeField] private Image _HP_2;
    [SerializeField] private Image _HP_3;
    [SerializeField] private Sprite _undestroyedPortal;
    [SerializeField] private Sprite _destroyedPortal;

    private void Start()
    {
        switch (GameData.globalHP)
        {
            case 3:
                _HP_1.sprite = _undestroyedPortal;
                _HP_2.sprite = _undestroyedPortal;
                _HP_3.sprite = _undestroyedPortal;
                break;
            case 2:
                _HP_1.sprite = _destroyedPortal;
                _HP_2.sprite = _undestroyedPortal;
                _HP_3.sprite = _undestroyedPortal;
                break;
            case 1:
                _HP_1.sprite = _destroyedPortal;
                _HP_2.sprite = _destroyedPortal;
                _HP_3.sprite = _undestroyedPortal;
                break;
        }
    }
}
