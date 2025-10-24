using System;
using UnityEngine;

public class EnergySystem
{
    public float currentEnergy;
    public float maxEnergy = 100f;

    private float _speedUpdateEnergy = 20f;

    public event Action<float> OnEnergyChangedEvent;

    public EnergySystem()
    {
        currentEnergy = maxEnergy;
    }

    public void ConsumeEnergy(float amount)
    {
        currentEnergy = Mathf.Max(0, currentEnergy - amount);
        NotifyEnergyChanged();
    }

    public void UpdateEnergy(float deltaTime)
    {
        if (currentEnergy < maxEnergy)
        {
            float rechargeAmount = _speedUpdateEnergy * deltaTime;
            currentEnergy = Mathf.Min(maxEnergy, currentEnergy + rechargeAmount);
            NotifyEnergyChanged();
        }
    }

    private void NotifyEnergyChanged()
    {
        OnEnergyChangedEvent?.Invoke(currentEnergy); // ---> Player
    }
}
