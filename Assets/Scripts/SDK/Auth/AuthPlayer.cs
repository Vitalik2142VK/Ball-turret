using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class AuthPlayer : MonoBehaviour
{
    [SerializeField] private AuthPlayerView _authPlayerView;
    [SerializeField] private Button _authButton;
    [SerializeField, Min(20f)] private float _maxWaitTimeAuth;
    [SerializeField, Min(1f)] private float _waitTimeAuth;

    private WaitForSeconds _wait;
    private float _currentWaitTime;

    private void OnValidate()
    {
        if (_authPlayerView == null)
            throw new NullReferenceException(nameof(_authPlayerView));

        if (_authButton == null)
            throw new NullReferenceException(nameof(_authButton));
    }

    private void Awake()
    {
        _wait = new WaitForSeconds(_waitTimeAuth);
        _currentWaitTime = 0;
    }

    private void OnEnable()
    {
        _authButton.onClick.AddListener(OnAuthorize);
    }

    private void OnDisable()
    {
        _authButton.onClick.RemoveListener(OnAuthorize);
    }

    public void Authorize()
    {
        if (YandexGame.auth)
            FillData();
        else
            ShowAuthButton();
    }

    private void FillData()
    {
        if (_authPlayerView.IsAuthorized)
            return;

        var namePlayer = YandexGame.playerName;
        var urlIconPlayer = YandexGame.playerPhoto;

        _authPlayerView.SetDataAuthPlayer(urlIconPlayer, namePlayer);
        _authButton.gameObject.SetActive(false);
    }

    private void ShowAuthButton()
    {
        _authButton.gameObject.SetActive(true);
        _authPlayerView.gameObject.SetActive(false);
    }

    private void OnAuthorize()
    {
        YandexGame.AuthDialog();

        StartCoroutine(WaitAuth());
    }

    private IEnumerator WaitAuth()
    {
        while (YandexGame.auth == false && _currentWaitTime < _maxWaitTimeAuth)
        {
            _currentWaitTime += _waitTimeAuth;

            yield return _wait;
        }

        Authorize();
    }
}