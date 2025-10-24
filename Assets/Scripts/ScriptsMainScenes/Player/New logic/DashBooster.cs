using System.Collections;
using UnityEngine;

public class DashBooster
{
    private readonly TrailRenderer _trailRenderer;
    private readonly EnergySystem _energySystem;
    private readonly float _dashMultiplier;
    private readonly float _costEnergyOfDash;

    private float _dashTime = 0.1f;
    private float _dashRate = 1f;
    private bool _isDash = false;


    public DashBooster(TrailRenderer trailRenderer, EnergySystem energySystem, float dashMultiplier, float costEnergyOfDash)
    {
        _trailRenderer = trailRenderer;
        _energySystem = energySystem;
        _dashMultiplier = dashMultiplier;
        _costEnergyOfDash = costEnergyOfDash;
    }

    public void Activate()
    {
        if (_energySystem.currentEnergy > _costEnergyOfDash && !_isDash)
        {
            _trailRenderer.emitting = true;
            _isDash = true;
            Player.Instance.moveSpeed *= _dashMultiplier;
            _energySystem.ConsumeEnergy(_costEnergyOfDash);

            CoroutineRunner.Instance.RunCoroutine(ResetSpeedRoutine());
        }
    }

    private IEnumerator ResetSpeedRoutine()
    {
        yield return new WaitForSeconds(_dashTime);

        _trailRenderer.emitting = false;
        Player.Instance.moveSpeed /= _dashMultiplier;

        yield return new WaitForSeconds(_dashRate);

        _isDash = false;
    }
}
