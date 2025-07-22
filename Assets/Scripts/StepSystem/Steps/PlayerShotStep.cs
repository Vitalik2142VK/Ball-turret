using System;

public class PlayerShotStep : IStep
{
    private IPlayerController _player;

    public PlayerShotStep(IPlayerController player)
    {
        _player = player ?? throw new ArgumentNullException(nameof(player));
    }

    public void Action()
    {
        _player.SelectTarget();
    }
}
