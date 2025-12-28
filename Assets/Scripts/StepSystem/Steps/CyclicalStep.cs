using System;

public class CyclicalStep : IStep
{
    private IActorsController _actorsController;
    private ITurretState _turretState;
    private IDynamicEndStep _dynamicEndStep;
    private IStep _startStep;
    private IStep _loopingStep;
    private IStep _finishStep;

    public CyclicalStep(IActorsController actorController, IDynamicEndStep dynamicEndStep, ITurretState turretState)
    {
        _actorsController = actorController ?? throw new ArgumentNullException(nameof(actorController));
        _dynamicEndStep = dynamicEndStep ?? throw new ArgumentNullException(nameof(dynamicEndStep));
        _turretState = turretState ?? throw new ArgumentNullException(nameof(turretState));
    }

    public void SetStartStep(IStep startStep)
    {
        _startStep = startStep ?? throw new ArgumentNullException(nameof(startStep));
    }

    public void SetLoopingStep(IStep loopingStep)
    {
        _loopingStep = loopingStep ?? throw new ArgumentNullException(nameof(loopingStep));
    }

    public void SetFinishStep(IStep finishStep)
    {
        _finishStep = finishStep ?? throw new ArgumentNullException(nameof(finishStep));
    }

    public void Action()
    {
        if (_actorsController.AreNoEnemies && _actorsController.AreWavesOver || _turretState.IsDestroyed)
        {
            _dynamicEndStep.SetNextStep(_finishStep);
        }
        else if (_actorsController.AreNoEnemies)
        {
            _actorsController.Reboot();
            _dynamicEndStep.SetNextStep(_startStep);
        }
        else
        {
            _dynamicEndStep.SetNextStep(_loopingStep);
        }

        _dynamicEndStep.End();
    }
}
