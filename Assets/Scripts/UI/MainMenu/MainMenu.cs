using System;
using UnityEngine;

public class MainMenu : MonoBehaviour, IMenu
{
    [SerializeField] private PlayMenu _playMenu;
    [SerializeField] private ImprovementMenu _improvementMenu;
    [SerializeField] private SettingMenu _settingMenu;

    private GameObject _gameObject;

    private void OnValidate()
    {
        if (_playMenu == null)
            throw new NullReferenceException(nameof(_playMenu));

        if (_improvementMenu == null)
            throw new NullReferenceException(nameof(_improvementMenu));

        if (_settingMenu == null)
            throw new NullReferenceException(nameof(_settingMenu));
    }

    private void Awake()
    {
        _gameObject = gameObject;
    }

    public void OnOpenPlayMenu()
    {
        _gameObject.SetActive(false);
        _playMenu.Open(this);
    }

    public void OnOpenShopMenu()
    {
        _gameObject.SetActive(false);
        _improvementMenu.Open(this);
    }

    public void OnOpenSettingMenu()
    {
        _gameObject.SetActive(false);
        _settingMenu.Open(this);
    }

    public void OnOpenLeaderboard()
    {
        throw new NotImplementedException();
    }

    public void Enable()
    {
        _gameObject.SetActive(true);
    }
}
