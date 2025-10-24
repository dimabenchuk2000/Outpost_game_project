using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class KnockBack : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private float _knockBackForce = 3f;
    [SerializeField] private float _knockBackMovingTimerMax = 0.3f;

    [HideInInspector] public bool _isKnockBack = false;

    private float _knockBackMovingTimer;
    private Rigidbody2D _rb;
    // ----------------------------------

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckStopKnockBackMovement();
    }

    // Поле публичных методов
    public bool IsKnockBack() => _isKnockBack;

    public void GetKnockBack(Transform sourceDamage)
    {
        _isKnockBack = true;
        _knockBackMovingTimer = _knockBackMovingTimerMax;
        Vector2 difference = (transform.position - sourceDamage.position).normalized * _knockBackForce / _rb.mass;

        _rb.AddForce(difference, ForceMode2D.Impulse);
    }

    public void StopKnockBackMovement()
    {
        _rb.linearVelocity = Vector2.zero;
        _isKnockBack = false;
    }
    // -----------------------------------

    // Поле приватных методов
    private void CheckStopKnockBackMovement()
    {
        _knockBackMovingTimer -= Time.deltaTime;

        if (_knockBackMovingTimer < 0)
        {
            StopKnockBackMovement();
        }
    }
    // ----------------------------------
}
