using CannonTurret.UI;
using UnityEngine.SceneManagement;

namespace CannonTurret.LevelSystem
{
    public class StartSceneLoader : ISceneLoader
    {
        public void Load()
        {
            SceneManager.LoadScene((int)SceneIndex.StartScene);
        }
    }
}