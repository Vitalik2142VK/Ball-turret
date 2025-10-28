using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private MainMenuLoader _mainMenuLoader;

    private void OnValidate()
    {
        if (_mainMenuLoader == null)
            throw new System.NullReferenceException(nameof(_mainMenuLoader));
    }

    private void Start()
    {
        _mainMenuLoader.Load();
    }
}
