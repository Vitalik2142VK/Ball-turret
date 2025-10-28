using System;

public class PlayerStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IPlayerController _playerController;
    private IActorsController _actorsController;

    public PlayerStep(IPlayerController playerController, IActorsController actorsController)
    {
        _playerController = playerController ?? throw new ArgumentNullException(nameof(playerController));
        _actorsController = actorsController ?? throw new ArgumentNullException(nameof(actorsController));
    }

    public void Action()
    {
        _playerController.SelectTarget();

        if (_actorsController.AreNoEnemies)
            _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}
