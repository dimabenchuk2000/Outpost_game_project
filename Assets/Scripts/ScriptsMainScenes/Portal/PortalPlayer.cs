using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PortalPlayer : MonoBehaviour, IDamageable
{
    public static PortalPlayer Instance;

    // Поле переменных
    [SerializeField] private int _portalHealth = 10;

    public bool isPortalDestruction = false;

    private Animator _animator;

    private int _currentPortalHealth;

    private static string IS_TAKE_DAMAGE = "isTakeDamage";
    private static string IS_DEAD = "isDead";
    // ----------------------------------

    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentPortalHealth = _portalHealth;
    }

    // Поле публичных методов
    public void TakeDamage(int damage, Transform sourceDamage)
    {
        if (isPortalDestruction == false)
        {
            _currentPortalHealth -= damage;
            _animator.SetTrigger(IS_TAKE_DAMAGE);
            DetectDestructionPortal();
        }

    }
    // ----------------------------------

    // Поле приватных методов
    private void DetectDestructionPortal()
    {
        if (_currentPortalHealth <= 0)
        {
            Player.Instance.isPlayerDead = true;
            CameraPortal.Instance.CameraPortalOn();
            GameData.globalHP -= 1;
            isPortalDestruction = true;
            _animator.SetBool(IS_DEAD, true);
            StartCoroutine(UIAfterDestroy());
        }
    }
    // ----------------------------------

    IEnumerator UIAfterDestroy()
    {
        yield return new WaitForSeconds(2f);
        UIAfterDestroyPortal.Instance.LightenTextYouDead();
        yield return new WaitForSeconds(2f);

        if (GameData.globalHP < 1)
        {
            UIAfterDestroyPortal.Instance.LightenTextEnd();
            ResetProgress.ResetAll();
        }
        else
            UIAfterDestroyPortal.Instance.LightenTextButNotEnd();

        yield return new WaitForSeconds(2f);
        GameInput.Instance.GoToBase();
    }
}
