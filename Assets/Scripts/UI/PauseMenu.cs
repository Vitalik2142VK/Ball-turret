using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, IMenu
{
    [SerializeField] private Pause _pause;
    [SerializeField] private SettingMenu _settingMenu;

    private GameObject _gameObject;

    private void OnValidate()
    {
        if (_pause == null)
            throw new System.NullReferenceException(nameof(_pause));

        if (_settingMenu == null)
            throw new System.NullReferenceException(nameof(_settingMenu));
    }

    private void Awake()
    {
        _gameObject = gameObject;
        _gameObject.SetActive(false);
    }

    public void Enable()
    {
        _gameObject.SetActive(true);
    }

    public void Open()
    {
        _pause.Enable();
        _gameObject.SetActive(true);
    }

    public void Play()
    {
        _gameObject.SetActive(false);
        _pause.Disable();
    }

    public void OpenSettingMenu()
    {
        _gameObject.SetActive(false);
        _settingMenu.Open(this);
    }

    public void Exit()
    {
        Debug.Log("Change scene to Main menu.");
    }
}
