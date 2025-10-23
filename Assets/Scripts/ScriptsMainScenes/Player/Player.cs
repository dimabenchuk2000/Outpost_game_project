using System;
using System.Collections;
using Outpost.Player_Attack;
using Outpost.Player_Movement;
using Outpost.Player_ResourceTransfer;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(KnockBack))]

public class Player : MonoBehaviour
{
    public static Player Instance;

    // Поле переменных
    [SerializeField] private Text _textHP;
    [SerializeField] private Image _HPBar;

    [SerializeField] private Text _textEnergy;
    [SerializeField] private Image _EnergyBar;

    [SerializeField] private TrailRenderer trailRenderer;

    public KnockBack _knockBack;

    public float currentEnergy;

    public bool _isPlayerMove = true;
    public bool isPlayerTPBase = false;
    public bool isShiftHeld = false;
    public bool isPlayerDead = false;

    private Rigidbody2D _rb;

    private Vector2 _vectorDirectionMovement;

    private float _playerHealth = 10f;
    private float _currentHealth;

    private float _playerEnergy = 100f;
    private float _timeEnergyRate = 0.05f;
    private float _recoveryEnergyAmount = 1f;
    private float _costEnergyOfDash = 60;
    // ----------------------------------

    // Поле событий
    public event EventHandler OnPlayerTakeDamage;
    // ----------------------------------

    private void Awake()
    {
        _knockBack = GetComponent<KnockBack>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Instance = this;
        _currentHealth = _playerHealth;
        _textHP.text = _currentHealth.ToString() + "/" + _playerHealth.ToString();

        currentEnergy = _playerEnergy;
        _textEnergy.text = currentEnergy.ToString() + "/" + _playerEnergy.ToString();

        Player_Attack._isPlayerFightMode = false;

        GameInput.Instance.OnPlayerFightMode += Player_Attack.GameInput_OnPlayerFightMode;
        GameInput.Instance.OnPlayerAttackTop += Player_Attack.GameInput_OnPlayerAttackTop;
        GameInput.Instance.OnPlayerAttackDown += Player_Attack.GameInput_OnPlayerAttackDown;
        GameInput.Instance.OnPlayerDashPerformed += Player_Movement.GameInput_OnPlayerDashPerformed;
        GameInput.Instance.OnPlayerRunPerformed += Player_Movement.GameInput_OnPlayerRunPerformed;
        GameInput.Instance.OnPlayerRunCancaled += Player_Movement.GameInput_OnPlayerRunCancaled;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnPlayerFightMode -= Player_Attack.GameInput_OnPlayerFightMode;
        GameInput.Instance.OnPlayerAttackTop -= Player_Attack.GameInput_OnPlayerAttackTop;
        GameInput.Instance.OnPlayerAttackDown -= Player_Attack.GameInput_OnPlayerAttackDown;
        GameInput.Instance.OnPlayerDashPerformed -= Player_Movement.GameInput_OnPlayerDashPerformed;
        GameInput.Instance.OnPlayerRunPerformed -= Player_Movement.GameInput_OnPlayerRunPerformed;
        GameInput.Instance.OnPlayerRunCancaled -= Player_Movement.GameInput_OnPlayerRunCancaled;

        CancelInvoke();
    }

    private void Update()
    {
        _vectorDirectionMovement = GameInput.Instance.GetVectorDirectionMovement();
    }

    private void FixedUpdate()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        if (_isPlayerMove)
            Player_Movement.PlayerMove();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PortalPlayer"))
        {
            Player_ResourceTransfer.ResourceTransfer();
            BtnEnabled.Instance.ButtonGoToBaseOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PortalPlayer"))
        {
            Player_ResourceTransfer.ResourceTransfer();
            BtnEnabled.Instance.ButtonGoToBaseOff();
        }
    }

    // Поле публичных методов
    public Rigidbody2D Rigidbody2D() => _rb;
    public Vector2 VectorDirectionMovement() => _vectorDirectionMovement;
    public bool IsPlayerDead() => isPlayerDead;

    public void IsFightModeOff()
    {
        StartCoroutine(IsPlayerFightModeOff());
    }

    public void DashOn()
    {
        if (currentEnergy >= _costEnergyOfDash)
            StartCoroutine(DashRoutine());
    }

    public void TakeDamage(int damage, Transform sourceDamage)
    {
        if (!isPlayerDead)
        {
            _currentHealth -= damage;
            _knockBack.GetKnockBack(sourceDamage);
            OnPlayerTakeDamage?.Invoke(this, EventArgs.Empty); // ---> PlayerVisual
            _textHP.text = _currentHealth.ToString() + "/" + _playerHealth.ToString();
            _HPBar.fillAmount = _currentHealth / _playerHealth;
            DetectDeath();
        }
    }

    public void LossEnergyRunning()
    {
        CancelInvoke("DecreaseEnergy");
        CancelInvoke("RecoverEnergy");
        InvokeRepeating("DecreaseEnergy", 0, 0.05f);
    }

    public void AddEnergy()
    {
        CancelInvoke("DecreaseEnergy");
        CancelInvoke("RecoverEnergy");
        InvokeRepeating("RecoverEnergy", 0, _timeEnergyRate);
    }
    // ----------------------------------

    // Поле приватных методов
    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            _knockBack.StopKnockBackMovement();
            isPlayerDead = true;
            StartCoroutine(UIAfterDead());
        }
    }

    private void RecoverEnergy()
    {
        currentEnergy = Mathf.Min(currentEnergy + _recoveryEnergyAmount, _playerEnergy);
        _textEnergy.text = currentEnergy.ToString() + "/" + _playerEnergy.ToString();
        _EnergyBar.fillAmount = currentEnergy / _playerEnergy;
    }

    private void DecreaseEnergy()
    {
        currentEnergy = currentEnergy - 1;
        _textEnergy.text = currentEnergy.ToString() + "/" + _playerEnergy.ToString();
        _EnergyBar.fillAmount = currentEnergy / _playerEnergy;

        if (currentEnergy <= 0)
        {
            isShiftHeld = false;
            AddEnergy();
            Player_Movement.RunCheck();
        }
    }
    // ----------------------------------

    // Поле корутин
    IEnumerator IsPlayerFightModeOff()
    {
        yield return new WaitForSeconds(1f);
        Player_Attack._isPlayerFightMode = false;
    }

    IEnumerator DashRoutine()
    {
        currentEnergy -= _costEnergyOfDash;
        CancelInvoke("RecoverEnergy");
        InvokeRepeating("RecoverEnergy", 0, _timeEnergyRate);
        trailRenderer.emitting = true;
        Player_Movement.movementSpeed *= Player_Movement.dashMultiplier;

        yield return new WaitForSeconds(Player_Movement.dashTime);

        Player_Movement.movementSpeed /= Player_Movement.dashMultiplier;
        trailRenderer.emitting = false;
    }

    IEnumerator UIAfterDead()
    {
        yield return new WaitForSeconds(2f);
        ScreenDarken.Instanse.DarkenScreen();
        yield return new WaitForSeconds(2.5f);
        UIAfterDeadPlayer.Instance.LightenTextYouDead();
        yield return new WaitForSeconds(2f);
        UIAfterDeadPlayer.Instance.LightenTextButNotEnd();
        yield return new WaitForSeconds(2f);
        GameInput.Instance.GoToBase();
    }
    // ----------------------------------
}
