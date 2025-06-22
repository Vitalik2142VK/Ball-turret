using System;

public class PlayerShotStep : IStep
{
    private IDesignator _player;

    public PlayerShotStep(IDesignator player)
    {
        _player = player ?? throw new ArgumentNullException(nameof(player));
    }

    public void Action()
    {
        _player.SelectTarget();
    }
}
