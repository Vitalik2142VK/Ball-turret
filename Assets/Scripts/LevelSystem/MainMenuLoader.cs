using CannonTurret.UI;
using UnityEngine.SceneManagement;

namespace CannonTurret.LevelSystem
{
    public class MainMenuLoader : ISceneLoader
    {
        public void Load()
        {
            SceneManager.LoadScene((int)SceneIndex.MainMenuScene);
        }
    }
}