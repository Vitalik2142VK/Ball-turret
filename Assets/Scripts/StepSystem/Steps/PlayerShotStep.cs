using System;

public class PlayerShotStep : IStep
{
    private IPlayer _player;

    public PlayerShotStep(IPlayer player)
    {
        _player = player ?? throw new ArgumentNullException(nameof(player));
    }

    public void Action()
    {
        _player.SelectTarget();
    }
}
