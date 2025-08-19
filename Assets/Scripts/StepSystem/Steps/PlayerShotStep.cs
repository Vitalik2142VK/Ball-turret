using System;

public class PlayerShotStep : IStep
{
    private IPlayerController _playerController;

    public PlayerShotStep(IPlayerController playerController)
    {
        _playerController = playerController ?? throw new ArgumentNullException(nameof(playerController));
    }

    public void Action()
    {
        _playerController.SelectTarget();
    }
}
