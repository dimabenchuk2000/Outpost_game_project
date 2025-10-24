using System;
using Outpost.Player_Attack;
using Outpost.Player_Movement;
using UnityEngine;

public class Bow : MonoBehaviour
{
    // Поле переменных
    private Quaternion _transRot;
    // ----------------------------------

    // Поле событий
    public event EventHandler OnFightMode;
    public event EventHandler OnAttack;
    // ----------------------------------

    private void Start()
    {
        _transRot = transform.rotation;
    }

    private void Update()
    {
        if (Player_Attack.IsPlayerFightMode())
            RotationBow();

        if (Player_Attack.IsPlayerFightMode() == false)
            SetStartPosBow();
    }

    // Поле публичных методов
    public void FightMode()
    {
        OnFightMode?.Invoke(this, EventArgs.Empty); // ---> BowVisual
    }

    public void Attack()
    {
        OnAttack?.Invoke(this, EventArgs.Empty); // ---> BowVisual
    }
    // ----------------------------------

    private void RotationBow()
    {
        {
            Vector3 mousePosition = GameInput.Instance.GetMousePosition();
            Vector3 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 direction = new Vector2(mousePosition.x - objectPosition.x, mousePosition.y - objectPosition.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void SetStartPosBow()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Camera.main.WorldToScreenPoint(Player.Instance.transform.position);

        if (mousePos.x < playerPos.x)
        {
            transform.rotation = _transRot;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
            transform.rotation = _transRot;
    }
}
