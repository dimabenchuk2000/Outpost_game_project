public class RunBooster
{
    private readonly EnergySystem _energySystem;
    private readonly float _runMultiplier;
    private readonly float _costEnergyOfRun;

    public bool isRunBoost = false;

    public RunBooster(EnergySystem energySystem, float runMultiplier, float costEnergyOfRun)
    {
        _energySystem = energySystem;
        _runMultiplier = runMultiplier;
        _costEnergyOfRun = costEnergyOfRun;
    }

    public void Activate()
    {
        if (_energySystem.currentEnergy > _costEnergyOfRun)
        {
            Player.Instance.moveSpeed *= _runMultiplier;
            isRunBoost = true;
            _energySystem.WasteEnergy(_costEnergyOfRun);
        }
    }

    public void Deactivate()
    {
        Player.Instance.moveSpeed /= _runMultiplier;
        isRunBoost = false;
        _energySystem.StopWasteEnergy();
    }
}
