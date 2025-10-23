using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PolygonCollider2D))]

public class SwordVisual : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private Sword _sword;

    private Animator _animator;
    private PolygonCollider2D _polygonCollider2D;

    private const string IS_FIGHT_MODE = "isFightMode";
    private const string IS_ATTACK_TOP = "isAttackTop";
    private const string IS_ATTACK_DOWN = "isAttackDown";
    private const string IS_SPEED_ATTACK = "isSpeedAttack";
    // ----------------------------------

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        _sword.OnFightMode += Sword_OnFightMode;
        _sword.OnAttackTop += Sword_OnAttackTop;
        _sword.OnAttackDown += Sword_OnAttackDown;
    }

    private void OnDestroy()
    {
        _sword.OnFightMode -= Sword_OnFightMode;
        _sword.OnAttackTop -= Sword_OnAttackTop;
        _sword.OnAttackDown -= Sword_OnAttackDown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_sword.transform.parent.tag == "Enemy")
        {
            if (collision.transform.TryGetComponent(out Player Player))
                Player.TakeDamage(_sword.SwordDamage(), _sword.transform.parent.transform);

            if (collision.transform.TryGetComponent(out PortalPlayer PortalPlayer))
                PortalPlayer.PortalTakeDamage(_sword.SwordDamage());

            if (collision.transform.TryGetComponent(out AlliesEntity Allies))
                Allies.TakeDamage(_sword.SwordDamage(), _sword.transform.parent.transform);
        }
        else
        {
            if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
                enemyEntity.TakeDamage(_sword.SwordDamage(), Player.Instance.transform);
        }
    }

    // Поле публичных методов
    public void AttackColliderOff()
    {
        _polygonCollider2D.enabled = false;
    }
    public void AttackColliderOn()
    {
        _polygonCollider2D.enabled = true;
    }
    public void AttackColliderOffOn()
    {
        AttackColliderOff();
        AttackColliderOn();
    }
    // ----------------------------------

    // Поле приватных методов
    private void Sword_OnFightMode(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_FIGHT_MODE);
    }

    private void Sword_OnAttackTop(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_ATTACK_TOP);
        _animator.SetFloat(IS_SPEED_ATTACK, _sword.SwordSpeedAttack());
    }

    private void Sword_OnAttackDown(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_ATTACK_DOWN);
        _animator.SetFloat(IS_SPEED_ATTACK, _sword.SwordSpeedAttack());
    }
    // ----------------------------------
}
