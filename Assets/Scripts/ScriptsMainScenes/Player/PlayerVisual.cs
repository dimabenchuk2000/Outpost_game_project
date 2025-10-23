using System;
using Outpost.Player_Movement;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class PlayerVisual : MonoBehaviour
{
    // Поле переменных
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private const string IS_RUNNING = "isRunning";
    private const string IS_DEAD = "isDead";
    private const string IS_TAKE_DAMAGE = "isTakeDamage";
    private const string IS_TP_BASE = "isTPBase";
    // ----------------------------------

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Player.Instance.OnPlayerTakeDamage += Player_OnPlayerTakeDamage;
        GameInput.Instance.OnPlayerTPBase += GameInput_OnPlayerTPBase;
    }

    private void OnDestroy()
    {
        Player.Instance.OnPlayerTakeDamage -= Player_OnPlayerTakeDamage;
        GameInput.Instance.OnPlayerTPBase -= GameInput_OnPlayerTPBase;
    }

    private void Update()
    {
        PlayerRotation();
        PlayerRunning();
        PlayerDead();
    }

    public void DestroyPlayer()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void TPBase()
    {
        Player.Instance.isPlayerTPBase = false;
        SceneManager.LoadScene("PlayerBase");
    }

    // Поле приватных методов
    private void PlayerRotation()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player_Movement.GetPlayerPosition();

        if (mousePos.x < playerPos.x)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }

    private void PlayerRunning()
    {
        _animator.SetBool(IS_RUNNING, Player_Movement.IsPlayerRunning());
    }

    private void PlayerDead()
    {
        _animator.SetBool(IS_DEAD, Player.Instance.IsPlayerDead());
    }

    private void Player_OnPlayerTakeDamage(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_TAKE_DAMAGE);
    }

    private void GameInput_OnPlayerTPBase(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_TP_BASE);
    }
    // ----------------------------------
}
