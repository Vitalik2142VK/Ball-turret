using System;

public class CyclicalStep : IStep
{
    private IActorsController _actorsController;
    private IDynamicEndStep _dynamicEndStep;
    private IWinState _winState;

    private IStep _startStep;
    private IStep _playerStep;
    private IStep _finishStep;

    public CyclicalStep(IActorsController actorController, IDynamicEndStep dynamicEndStep, IWinState winState)
    {
        _actorsController = actorController ?? throw new ArgumentNullException(nameof(actorController));
        _dynamicEndStep = dynamicEndStep ?? throw new ArgumentNullException(nameof(dynamicEndStep));
        _winState = winState ?? throw new ArgumentNullException(nameof(winState));
    }

    public void SetStartStep(IStep startStep)
    {
        _startStep = startStep ?? throw new ArgumentNullException(nameof(startStep));
    }

    public void SetPlayerStep(IStep playerStep)
    {
        _playerStep = playerStep ?? throw new ArgumentNullException(nameof(playerStep));
    }

    public void SetFinishStep(IStep finishStep)
    {
        _finishStep = finishStep ?? throw new ArgumentNullException(nameof(finishStep));
    }

    public void Action()
    {
        if (_actorsController.AreNoEnemies && _actorsController.AreWavesOver || _winState.IsWin == false)
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
            _dynamicEndStep.SetNextStep(_playerStep);
        }

        _dynamicEndStep.End();
    }
}
