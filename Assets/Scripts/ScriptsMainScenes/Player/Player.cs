using System;
using System.Collections;
using Outpost.Player_Attack;
using Outpost.Player_ResourceTransfer;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(KnockBack))]

public class Player : MonoBehaviour
{
    public static Player Instance;

    // Поле переменных
    [Header("UI(dont touch)")]
    [SerializeField] private Text _textHP;
    [SerializeField] private Image _HPBar;
    [SerializeField] private Text _textEnergy;
    [SerializeField] private Image _EnergyBar;

    [Header("TrailRenderer(dont touch)")]
    [SerializeField] private TrailRenderer _trailRenderer;

    [Header("Dash settings")]
    [SerializeField] private float _dashMultiplier = 4;
    [SerializeField] private float _costEnergyOfDash = 40;

    [Header("RunBoost settings")]
    [SerializeField] private float _runMultiplier = 4;
    [SerializeField] private float _costEnergyOfRun = 1;

    [Header("RunSpeed")]
    public float moveSpeed = 5;

    [HideInInspector] public bool isPlayerMove = true;
    [HideInInspector] public bool isPlayerTPBase = false;
    [HideInInspector] public bool isPlayerDead = false;
    [HideInInspector] public bool isPlayerRunning = false;

    private DirectionalMover _mover;
    private DashBooster _dash;
    private EnergySystem _energySystem;
    private RunBooster _runBoost;

    private Rigidbody2D _rb;
    private KnockBack _knockBack;

    private float _playerHealth = 10f;
    private float _currentHealth;
    // ----------------------------------

    // Поле событий
    public event EventHandler OnPlayerTakeDamage;
    // ----------------------------------

    private void Awake()
    {
        _knockBack = GetComponent<KnockBack>();
        _rb = GetComponent<Rigidbody2D>();

        _mover = new DirectionalMover(_rb);
        _energySystem = new EnergySystem();
        _dash = new DashBooster(_trailRenderer, _energySystem, _dashMultiplier, _costEnergyOfDash);
        _runBoost = new RunBooster(_energySystem, _runMultiplier, _costEnergyOfRun);
    }

    private void Start()
    {
        Instance = this;
        _currentHealth = _playerHealth;
        _textHP.text = _currentHealth.ToString() + "/" + _playerHealth.ToString();

        _textEnergy.text = _energySystem.currentEnergy.ToString() + "/" + _energySystem.maxEnergy.ToString();

        Player_Attack._isPlayerFightMode = false;

        GameInput.Instance.OnPlayerDashStarted += GameInput_OnPlayerDashStarted;
        GameInput.Instance.OnPlayerRunPerformed += GameInput_OnPlayerRunPerformed;
        GameInput.Instance.OnPlayerRunCancaled += GameInput_OnPlayerRunCancaled;

        _energySystem.OnEnergyChangedEvent += EnergySystem_OnEnergyChangedEvent;

        Player_Attack.SubscribeToEvents();
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnPlayerDashStarted -= GameInput_OnPlayerDashStarted;
        GameInput.Instance.OnPlayerRunPerformed -= GameInput_OnPlayerRunPerformed;
        GameInput.Instance.OnPlayerRunCancaled -= GameInput_OnPlayerRunCancaled;

        _energySystem.OnEnergyChangedEvent -= EnergySystem_OnEnergyChangedEvent;

        Player_Attack.UnsubscribeFromEvents();
    }

    private void FixedUpdate()
    {
        if (isPlayerMove && !_knockBack._isKnockBack)
            PlayerMove();

        if (!_runBoost.isRunBoost)
            _energySystem.UpdateEnergy(Time.fixedDeltaTime);

        if (_energySystem.currentEnergy <= 0)
            _runBoost.Deactivate();
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
            BtnEnabled.Instance.ButtonGoToBaseOff();
        }
    }

    // Поле публичных методов
    public bool IsPlayerDead() => isPlayerDead;

    public void IsFightModeOff()
    {
        StartCoroutine(IsPlayerFightModeOff());
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

    // Поле приватных методов
    private void PlayerMove()
    {
        _mover.SetInputDirection(GameInput.Instance.GetVectorDirectionMovement());
        _mover.Update(Time.fixedDeltaTime, moveSpeed);

        if (GameInput.Instance.GetVectorDirectionMovement().magnitude > 0)
            isPlayerRunning = true;
        else isPlayerRunning = false;
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            _knockBack.StopKnockBackMovement();
            isPlayerDead = true;
            StartCoroutine(UIAfterDead());
        }
    }

    private void GameInput_OnPlayerDashStarted(object sender, EventArgs e)
    {
        if (!_runBoost.isRunBoost)
            _dash.Activate();
    }

    private void GameInput_OnPlayerRunPerformed(object sender, EventArgs e)
    {
        if (!_dash.isDash)
            _runBoost.Activate();
    }

    private void GameInput_OnPlayerRunCancaled(object sender, EventArgs e)
    {
        if (_runBoost.isRunBoost && _energySystem.currentEnergy > 0)
            _runBoost.Deactivate();
    }

    private void EnergySystem_OnEnergyChangedEvent(float newEnergy)
    {
        _textEnergy.text = Mathf.CeilToInt(newEnergy).ToString() + "/" + _energySystem.maxEnergy.ToString();
        _EnergyBar.fillAmount = newEnergy / _energySystem.maxEnergy;
    }
    // ----------------------------------

    // Поле корутин
    IEnumerator IsPlayerFightModeOff()
    {
        yield return new WaitForSeconds(1f);
        Player_Attack._isPlayerFightMode = false;
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
