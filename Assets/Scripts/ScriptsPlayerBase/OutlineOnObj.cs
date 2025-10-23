using UnityEngine;

public class OutlineOnObj : MonoBehaviour
{
    public Sprite mainSprite;
    public Sprite outlineSprite;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        _spriteRenderer.sprite = outlineSprite;
    }

    private void OnMouseExit()
    {
        _spriteRenderer.sprite = mainSprite;
    }
}
