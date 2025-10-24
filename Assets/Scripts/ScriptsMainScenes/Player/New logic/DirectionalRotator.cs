using UnityEngine;

public class DirectionalRotator
{
    private SpriteRenderer _spriteRenderer;
    private Vector3 _currentCharacterPos;
    private Vector3 _currentMousePos;

    public DirectionalRotator(SpriteRenderer spriteRenderer)
    {
        _spriteRenderer = spriteRenderer;
    }

    public void SetCharacterPos(Vector3 direction) => _currentCharacterPos = direction;
    public void SetMousePos(Vector3 direction) => _currentMousePos = direction;

    public void Update()
    {
        if (_currentMousePos.x < _currentCharacterPos.x)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }
}
