using System;
using System.Collections;
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

    public void WasteEnergy(float costEnergy)
    {
        CoroutineRunner.Instance.RunCoroutine("DrainEnergy", DrainEnergy(costEnergy));
    }

    public void StopWasteEnergy()
    {
        CoroutineRunner.Instance.StoppedCoroutine("DrainEnergy");
    }

    // Приватные методы ----------------------
    private void NotifyEnergyChanged()
    {
        OnEnergyChangedEvent?.Invoke(currentEnergy); // ---> Player
    }

    // Корутины ----------------------
    private IEnumerator DrainEnergy(float costEnergy)
    {
        while (currentEnergy > 0)
        {
            yield return new WaitForSeconds(0.05f);
            currentEnergy = Mathf.Max(0, currentEnergy - costEnergy);
            OnEnergyChangedEvent?.Invoke(currentEnergy);
        }
    }
}
