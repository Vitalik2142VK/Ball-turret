using UnityEngine;

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
