using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class AuthPlayer : MonoBehaviour
{
    [SerializeField] private AuthPlayerView _authPlayerView;
    [SerializeField] private AuthWindow _authWindow;
    [SerializeField] private Button _authButton;

    private void OnValidate()
    {
        if (_authPlayerView == null)
            throw new NullReferenceException(nameof(_authPlayerView));

        if (_authWindow == null)
            throw new NullReferenceException(nameof(_authWindow));

        if (_authButton == null)
            throw new NullReferenceException(nameof(_authButton));
    }

    private void OnEnable()
    {
        _authButton.onClick.AddListener(OnOpenAuthWindow);
        YG2.onGetSDKData += OnFillData;
    }

    private void OnDisable()
    {
        _authButton.onClick.RemoveListener(OnOpenAuthWindow);
        YG2.onGetSDKData -= OnFillData;
    }

    public void Authorize()
    {
        if (YG2.player.auth)
            OnFillData();
        else
            ShowAuthButton();
    }

    private void ShowAuthButton()
    {
        _authButton.gameObject.SetActive(true);
        _authPlayerView.gameObject.SetActive(false);
    }

    private void OnOpenAuthWindow() => _authWindow.Open();

    private void OnFillData()
    {
        if (_authPlayerView.IsAuthorized)
            return;

        var playerData = YG2.player;
        var namePlayer = playerData.name;
        var urlIconPlayer = playerData.photo;

        _authPlayerView.gameObject.SetActive(true);
        _authPlayerView.SetDataAuthPlayer(urlIconPlayer, namePlayer);
        _authButton.gameObject.SetActive(false);
    }
}