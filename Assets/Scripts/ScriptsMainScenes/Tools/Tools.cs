using System;
using UnityEngine;

public class Tools : MonoBehaviour, IWeapon
{
    // Поле переменных
    [SerializeField] private ToolsSO _toolsSO;

    private float _toolsExtractionRate;
    private float _toolsSpeedExtraction;
    private int _toolsDamage;
    // ----------------------------------

    // Поле событий
    public event EventHandler OnFightMode;
    public event EventHandler OnExtraction;
    // ----------------------------------

    private void Start()
    {
        _toolsExtractionRate = _toolsSO.toolsExtractionRate;
        _toolsSpeedExtraction = _toolsSO.toolsSpeedExtraction;
        _toolsDamage = _toolsSO.toolsDamage;
    }

    // Поле публичных методов
    public void Attack(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Normal:
                Extraction();
                break;
            case AttackType.Additional:
                Extraction();
                break;
        }
    }

    public float GetAttackRate()
    {
        return _toolsExtractionRate;
    }

    public void FightMode()
    {
        OnFightMode?.Invoke(this, EventArgs.Empty); // ---> ToolsVisual
    }

    public float ToolsSpeedExtraction() => _toolsSpeedExtraction;
    public int ToolsDamage() => _toolsDamage;
    // ----------------------------------

    // Полу приватных методов
    private void Extraction()
    {
        OnExtraction?.Invoke(this, EventArgs.Empty); // ---> ToolsVisual
    }
    // ----------------------------------
}
