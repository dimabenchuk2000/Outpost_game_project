using UnityEngine;

public class DirectionalRotator
{
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _currentCharacterPos;
    private Vector3 _currentTargetPos;

    public DirectionalRotator(SpriteRenderer spriteRenderer, Transform transform)
    {
        _spriteRenderer = spriteRenderer;
        _transform = transform;
    }

    public void SetCharacterPos(Vector3 direction) => _currentCharacterPos = direction;
    public void SetTargetPos(Vector3 direction) => _currentTargetPos = direction;

    public void Update(bool isPlayer)
    {
        if (isPlayer)
        {
            if (_currentTargetPos.x < _currentCharacterPos.x)
                _spriteRenderer.flipX = true;
            else
                _spriteRenderer.flipX = false;
        }
        else
        {
            if (_currentTargetPos.x < _currentCharacterPos.x)
                _transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                _transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
