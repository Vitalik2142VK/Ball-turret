using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    private IMenu _previousMenu;

    private GameObject _gameObject;

    private void Awake()
    {
        _gameObject = gameObject;
        _gameObject.SetActive(false);
    }

    public void Back()
    {
        _gameObject.SetActive(false);
        _previousMenu.Enable();
    }

    public void Open(IMenu previousMenu)
    {
        _previousMenu = previousMenu ?? throw new System.ArgumentNullException(nameof(previousMenu));
        _gameObject.SetActive(true);
    }
}
