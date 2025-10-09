using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShiftAnimatorUI))]
public class MainMenu : MonoBehaviour, IMenu
{
    [SerializeField] private PlayMenu _playMenu;
    [SerializeField] private ImprovementMenu _improvementMenu;
    [SerializeField] private SettingMenu _settingMenu;
    [SerializeField] private LeaderboardWindow _leaderboardWindow;

    private IAnimatorUI _animator;

    private void OnValidate()
    {
        if (_playMenu == null)
            throw new NullReferenceException(nameof(_playMenu));

        if (_improvementMenu == null)
            throw new NullReferenceException(nameof(_improvementMenu));

        if (_settingMenu == null)
            throw new NullReferenceException(nameof(_settingMenu));

        if (_leaderboardWindow == null)
            throw new NullReferenceException(nameof(_leaderboardWindow));
    }

    private void Awake()
    {
        _animator = GetComponent<IAnimatorUI>();
    }

    public void OnOpenPlayMenu()
    {
        StartCoroutine(Close(_playMenu.Open));
    }

    public void OnOpenShopMenu()
    {
        StartCoroutine(Close(_improvementMenu.Open));
    }

    public void OnOpenSettingMenu()
    {
        StartCoroutine(Close(_settingMenu.Open));
    }

    public void OnOpenLeaderboard()
    {
        StartCoroutine(Close(_leaderboardWindow.Open));
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _animator.Show();
    }

    private IEnumerator Close(Action<IMenu> openOtherMenu)
    {
        _animator.Hide();

        yield return _animator.GetYieldAnimation();

        gameObject.SetActive(false);

        openOtherMenu?.Invoke(this);
    }
}
