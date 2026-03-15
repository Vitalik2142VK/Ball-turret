using CannonTurret.LevelSystem;
using UnityEngine;

namespace CannonTurret.EntryPoints
{
    public class GameStarter : MonoBehaviour
    {
        private MainMenuLoader _mainMenuLoader;

        private void Start()
        {
            if (_mainMenuLoader == null)
                _mainMenuLoader = new MainMenuLoader();

            _mainMenuLoader.Load();
        }
    }
}