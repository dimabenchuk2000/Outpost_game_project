using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class StaffVisual : MonoBehaviour
{
    [SerializeField] private Staff _staff;
    [SerializeField] private GameObject _magicBallPrefab;
    [SerializeField] private EnemyGoToPortalAI _enemyGoToPortalAI;

    private Animator _animator;

    private const string IS_ATTACK = "isAttack";
    private const string IS_FIGHT_MODE = "isFightMode";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _staff.OnAttack += Staff_OnAttack;
        _staff.OnFightMode += Staff_OnFightMode;
    }

    private void OnDestroy()
    {
        _staff.OnAttack -= Staff_OnAttack;
        _staff.OnFightMode -= Staff_OnFightMode;
    }

    private void Staff_OnAttack(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_ATTACK);
    }

    private void Staff_OnFightMode(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_FIGHT_MODE);
    }

    public void CreateMagicBall()
    {
        GameObject magicBall = Instantiate(_magicBallPrefab, new Vector3(transform.position.x, transform.position.y + .7f, transform.position.z), Quaternion.identity);
        magicBall.GetComponent<MagicBall>().SetTarget(_enemyGoToPortalAI.CurrentTarget());
        magicBall.GetComponent<MagicBall>().SetDamage(_staff.GetDamage());
    }
}
