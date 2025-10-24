using System.Collections;
using UnityEngine;

public class DashBooster
{
    private readonly TrailRenderer _trailRenderer;
    private readonly EnergySystem _energySystem;
    private readonly float _dashMultiplier;
    private readonly float _costEnergyOfDash;

    public bool isDash = false;

    private float _dashTime = 0.1f;
    private float _dashRate = 1f;

    public DashBooster(TrailRenderer trailRenderer, EnergySystem energySystem, float dashMultiplier, float costEnergyOfDash)
    {
        _trailRenderer = trailRenderer;
        _energySystem = energySystem;
        _dashMultiplier = dashMultiplier;
        _costEnergyOfDash = costEnergyOfDash;
    }

    public void Activate()
    {
        if (_energySystem.currentEnergy > _costEnergyOfDash && !isDash)
        {
            _trailRenderer.emitting = true;
            isDash = true;
            Player.Instance.moveSpeed *= _dashMultiplier;
            _energySystem.ConsumeEnergy(_costEnergyOfDash);

            CoroutineRunner.Instance.RunCoroutine("ResetSpeedRoutine", ResetSpeedRoutine());
        }
    }

    private IEnumerator ResetSpeedRoutine()
    {
        yield return new WaitForSeconds(_dashTime);

        _trailRenderer.emitting = false;
        Player.Instance.moveSpeed /= _dashMultiplier;

        yield return new WaitForSeconds(_dashRate);

        isDash = false;
        CoroutineRunner.Instance.StoppedCoroutine("ResetSpeedRoutine");
    }
}
