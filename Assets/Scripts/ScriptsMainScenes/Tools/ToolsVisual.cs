using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PolygonCollider2D))]

public class ToolsVisual : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private Tools _tools;

    private Animator _animator;
    private PolygonCollider2D _extractionCollider;

    private const string IS_FIGHT_MODE = "isFightMode";
    private const string IS_EXTRACTION = "isExtraction";
    private const string IS_SPEED_EXTRACTION = "isSpeedExtraction";
    // ----------------------------------

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _extractionCollider = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        _tools.OnFightMode += Tools_OnFightMode;
        _tools.OnExtraction += Tools_OnExtraction;
    }

    private void OnDestroy()
    {
        _tools.OnFightMode -= Tools_OnFightMode;
        _tools.OnExtraction -= Tools_OnExtraction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.tag == "Axe")
            if (collision.transform.TryGetComponent(out Environment Environment))
                if (Environment.transform.tag == "Tree")
                    Environment.TakeDamage(_tools.ToolsDamage());

        if (transform.tag == "Pickaxe")
            if (collision.transform.TryGetComponent(out Environment Environment))
                if (Environment.transform.tag == "Stone")
                    Environment.TakeDamage(_tools.ToolsDamage());
    }

    // Поле публичных методов
    public void ExtractionColliderOn()
    {
        _extractionCollider.enabled = true;
    }

    public void ExtractionColliderOff()
    {
        _extractionCollider.enabled = false;
    }

    public void ExtractionColliderOffOn()
    {
        _extractionCollider.enabled = false;
        _extractionCollider.enabled = true;
    }
    // ----------------------------------

    // Поле приватных методов
    private void Tools_OnFightMode(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_FIGHT_MODE);
    }

    private void Tools_OnExtraction(object sender, EventArgs e)
    {
        _animator.SetTrigger(IS_EXTRACTION);
        _animator.SetFloat(IS_SPEED_EXTRACTION, _tools.ToolsSpeedExtraction());
    }
    // ----------------------------------
}
