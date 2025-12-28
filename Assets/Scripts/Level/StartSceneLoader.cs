using UnityEngine.SceneManagement;

public class StartSceneLoader : ISceneLoader
{
    public void Load()
    {
        SceneManager.LoadScene((int)SceneIndex.StartScene);
    }
}