using UnityEngine;

public class DirectionalMover
{
    private Rigidbody2D _rb;
    private float _moveSpeed;
    private Vector2 _currentDiretion;

    public DirectionalMover(Rigidbody2D rb, float moveSpeed)
    {
        _rb = rb;
        _moveSpeed = moveSpeed;
    }

    public Vector2 CurrentVelocity => _currentDiretion.normalized * _moveSpeed;

    public void SetInputDirection(Vector2 direction) => _currentDiretion = direction;

    public void Update(float deltaTime)
    {
        _rb.MovePosition(_rb.position + _currentDiretion * (_moveSpeed * deltaTime));
    }
}
