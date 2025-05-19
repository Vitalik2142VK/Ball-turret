using System;
using UnityEngine;

public class MainMenu : MonoBehaviour, IMenu
{
    [SerializeField] private PlayMenu _playMenu;
    [SerializeField] private SettingMenu _settingMenu;

    private GameObject _gameObject;

    private void OnValidate()
    {
        if (_playMenu == null)
            throw new NullReferenceException(nameof(_playMenu));

        if (_settingMenu == null)
            throw new NullReferenceException(nameof(_settingMenu));
    }

    private void Awake()
    {
        _gameObject = gameObject;
    }

    public void OpenPlayMenu()
    {
        _gameObject.SetActive(false);
        _playMenu.Open(this);
    }

    public void OpenShopMenu()
    {
        Debug.Log("Open shop menu");
    }

    public void OpenSettingMenu()
    {
        _gameObject.SetActive(false);
        _settingMenu.Open(this);
    }

    public void Enable()
    {
        _gameObject.SetActive(true);
    }
}
