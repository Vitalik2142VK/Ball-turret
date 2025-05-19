using System;

public class CloseSceneStep : IStep
{
    private ISceneLoader _sceneLoader;
    private IUser _user;
    private ITurret _turret;

    public CloseSceneStep(ISceneLoader sceneLoader, IUser user, ITurret turret)
    {
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        _user = user ?? throw new ArgumentNullException(nameof(user));
        _turret = turret ?? throw new ArgumentNullException(nameof(turret));

        _turret.Destroyed += OnClose;
    }

    public void Disable()
    {
        _turret.Destroyed -= OnClose;
    }

    public void Action()
    {
        _user.IncreaseAchievedLevel();
        _sceneLoader.Load();
    }

    public void OnClose()
    {
        _sceneLoader.Load();
    }
}