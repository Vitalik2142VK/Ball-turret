using System;

public class CloseSceneStep : IStep
{
    private ISceneLoader _sceneLoader;

    public CloseSceneStep(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
    }

    public void Action()
    {
        _sceneLoader.Load();
    }
}
