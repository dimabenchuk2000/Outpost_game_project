using UnityEngine;

public class DirectionalMover
{
    private readonly Rigidbody2D _rb;

    private float _moveSpeed;
    private Vector2 _currentDiretion;

    public DirectionalMover(Rigidbody2D rb)
    {
        _rb = rb;
    }

    public Vector2 CurrentVelocity => _currentDiretion.normalized * _moveSpeed;

    public void SetInputDirection(Vector2 direction) => _currentDiretion = direction;

    public void Update(float deltaTime, float moveSpeed)
    {
        _moveSpeed = moveSpeed;
        _rb.MovePosition(_rb.position + _currentDiretion * (_moveSpeed * deltaTime));
    }
}
