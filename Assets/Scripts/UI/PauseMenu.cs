using UnityEngine;

public class PauseMenu : MonoBehaviour, IMenu
{
    [SerializeField] private Pause _pause;
    [SerializeField] private SettingMenu _settingMenu;

    private GameObject _gameObject;
    private CloseSceneStep _closeSceneStep;

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

    public void Initialize(CloseSceneStep closeSceneStep)
    {
        _closeSceneStep = closeSceneStep ?? throw new System.ArgumentNullException(nameof(closeSceneStep)); 
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
        _closeSceneStep.OnClose();
    }
}