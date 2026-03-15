using UnityEngine.SceneManagement;

public class MainMenuLoader : ISceneLoader
{
    public void Load()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenuScene);
    }
}
