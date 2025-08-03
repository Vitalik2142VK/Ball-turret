using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour, ISceneLoader
{
    public void Load()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenuScene);
    }
}
