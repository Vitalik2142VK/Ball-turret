using System;

public class PlayerStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IPlayerController _playerController;
    private IActorsController _actorsController;
    private IActivableUI _reservedBonusesWindow;

    public PlayerStep(IPlayerController playerController, IActorsController actorsController, IActivableUI reservedBonusesWindow)
    {
        _playerController = playerController ?? throw new ArgumentNullException(nameof(playerController));
        _actorsController = actorsController ?? throw new ArgumentNullException(nameof(actorsController));
        _reservedBonusesWindow = reservedBonusesWindow ?? throw new ArgumentNullException(nameof(reservedBonusesWindow));
    }

    public void Action()
    {
        if (_reservedBonusesWindow.IsActive == false)
            _playerController.SelectTarget();

        if (_actorsController.AreNoEnemies)
            _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}
