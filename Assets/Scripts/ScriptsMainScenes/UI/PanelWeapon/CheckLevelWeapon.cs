using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class CheckLevelWeapon : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteWeapon;
    [SerializeField] private string nameWeapon;

    private Image imageWeapon;

    private void Awake()
    {
        imageWeapon = GetComponent<Image>();
    }

    private void Start()
    {
        switch (GameData.weaponLevel[nameWeapon])
        {
            case 1:
                imageWeapon.sprite = spriteWeapon[0];
                break;
            case 2:
                imageWeapon.sprite = spriteWeapon[1];
                break;
            case 3:
                imageWeapon.sprite = spriteWeapon[2];
                break;
            case 4:
                imageWeapon.sprite = spriteWeapon[3];
                break;
            case 5:
                imageWeapon.sprite = spriteWeapon[4];
                break;
        }
    }
}
