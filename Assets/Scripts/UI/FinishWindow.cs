using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishWindow : MonoBehaviour, IMenu
{
    private const float EnableTimeScale = 1f;
    private const float DisableTimeScale = 0f;

    [SerializeField] private Button _videoViewingButton;
    [SerializeField] private TextMeshProUGUI _wonCoinsText;

    private IRewardIssuer _increaseRewardIssuer;

    private void OnValidate()
    {
        if (_videoViewingButton == null)
            throw new ArgumentNullException(nameof(_videoViewingButton));

        if (_wonCoinsText == null)
            throw new ArgumentNullException(nameof(_wonCoinsText));
    }

    private void Awake()
    {
        gameObject.SetActive(false);

        _videoViewingButton.interactable = true;
    }

    public void Initialize(IRewardIssuer increaseRewardIssuer)
    {
        _increaseRewardIssuer = increaseRewardIssuer ?? throw new ArgumentNullException(nameof(increaseRewardIssuer));
    }

    public void Enable()
    {
        Time.timeScale = DisableTimeScale;

        gameObject.SetActive(true);

        _wonCoinsText.text = _increaseRewardIssuer.GetReward().ToString();
    }

    public void OnWatchVideo()
    {
        bool isWatched = true;

        if (isWatched)
            _increaseRewardIssuer.ApplyDoublingReward();

        _videoViewingButton.interactable = false;
        _wonCoinsText.text = _increaseRewardIssuer.GetReward().ToString();
    }

    public void OnContinue()
    {
        _increaseRewardIssuer.AwardReward();

        gameObject.SetActive(false);

        Time.timeScale = EnableTimeScale;
    }
}
