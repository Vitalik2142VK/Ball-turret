public class ChangeSceneStep : IChangeSceneStep
{
    private ISceneLoader _sceneLoader;

    public void Action()
    {
        if (_sceneLoader == null)
            return;

        _sceneLoader.Load();
    }

    public void SetSceneLoader(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader ?? throw new System.ArgumentNullException(nameof(sceneLoader));
    }
}
