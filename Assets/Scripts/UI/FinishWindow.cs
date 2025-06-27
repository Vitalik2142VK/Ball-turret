using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishWindow : MonoBehaviour, IMenu
{
    private const RewardType PassingLevel = RewardType.PassingLevel;

    [SerializeField] private Pause _pause;
    [SerializeField] private Button _videoViewingButton;
    [SerializeField] private TextMeshProUGUI _wonBonusCoinsText;
    [SerializeField] private TextMeshProUGUI _wonCoinsText;

    private IRewardIssuer _increaseRewardIssuer;
    private IAdsViewer _adsViewer;

    private void OnValidate()
    {
        if (_pause == null)
            throw new ArgumentNullException(nameof(_pause));

        if (_videoViewingButton == null)
            throw new ArgumentNullException(nameof(_videoViewingButton));

        if (_wonBonusCoinsText == null)
            _wonBonusCoinsText = _videoViewingButton.transform.GetComponentInChildren<TextMeshProUGUI>();

        if (_wonBonusCoinsText == null)
            throw new ArgumentNullException(nameof(_wonBonusCoinsText));

        if (_wonCoinsText == null)
            throw new ArgumentNullException(nameof(_wonCoinsText));
    }

    private void Awake()
    {
        gameObject.SetActive(false);

        _videoViewingButton.interactable = true;
    }

    private void OnEnable()
    {
        if (_adsViewer != null)
            _adsViewer.RewardAdViewed += OnAddBonusReward;
    }

    private void OnDisable()
    {
        if (_adsViewer != null)
            _adsViewer.RewardAdViewed -= OnAddBonusReward;
    }

    public void Initialize(IRewardIssuer increaseRewardIssuer, IAdsViewer adsViewer)
    {
        _increaseRewardIssuer = increaseRewardIssuer ?? throw new ArgumentNullException(nameof(increaseRewardIssuer));
        _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
    }

    public void Enable()
    {
        _pause.Enable();
        gameObject.SetActive(true);
        _wonCoinsText.text = _increaseRewardIssuer.GetReward().ToString();

        if (_adsViewer.CanShowRewardAd)
        {
            _wonBonusCoinsText.text = $"+{_increaseRewardIssuer.GetBonusReward()}";
            _videoViewingButton.interactable = true;
        }
        else
        {
            _wonBonusCoinsText.text = $"0";
        }
    }

    public void OnWatchVideo()
    {
        _videoViewingButton.interactable = false;
        _adsViewer.ShowRewardAd(PassingLevel);
    }

    public void OnContinue()
    {
        if (_increaseRewardIssuer.IsRewardIssued == false)
            _increaseRewardIssuer.AwardReward();

        gameObject.SetActive(false);
        _adsViewer.ShowFullScreenAd();
        _pause.Disable();
    }

    private void OnAddBonusReward(RewardType reward)
    {
        if (reward != PassingLevel)
            return;

        _increaseRewardIssuer.AwarBonusReward();
        _wonCoinsText.text = _increaseRewardIssuer.GetReward().ToString();
        _increaseRewardIssuer.AwardReward();
    }
}
