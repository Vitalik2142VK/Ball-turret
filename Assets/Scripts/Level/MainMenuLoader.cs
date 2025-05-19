using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour, ISceneLoader
{
    private string NameScene = "MainMenuScene";

    public void Load()
    {
        SceneManager.LoadScene(NameScene);
    }
}
