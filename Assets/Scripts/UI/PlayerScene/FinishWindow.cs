using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishWindow : MonoBehaviour, IMenu
{
    private const RewardType PassingLevel = RewardType.AddCoin;

    [SerializeField] private Pause _pause;
    [SerializeField] private Button _videoViewingButton;
    [SerializeField] private TextMeshProUGUI _wonBonusCoinsText;
    [SerializeField] private TextMeshProUGUI _wonCoinsText;

    private IRewardIssuer _rewardIssuer;
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
        _videoViewingButton.interactable = false;
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

    public void Initialize(IRewardIssuer rewardIssuer, IAdsViewer adsViewer)
    {
        _rewardIssuer = rewardIssuer ?? throw new ArgumentNullException(nameof(rewardIssuer));
        _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
    }

    public void Enable()
    {
        _pause.Enable();
        gameObject.SetActive(true);

        if (_adsViewer.IsAdsDisable)
        {
            _wonCoinsText.text = _rewardIssuer.GetMaxReward().ToString();
            _rewardIssuer.PayMaxReward();
            _wonBonusCoinsText.text = $"0";
            _videoViewingButton.interactable = false;
        }
        else
        {
            _wonCoinsText.text = _rewardIssuer.GetReward().ToString();
            _rewardIssuer.PayReward();

            if (_adsViewer.CanShowRewardAd && _adsViewer.IsAdsDisable == false)
            {
                _wonBonusCoinsText.text = $"+{_rewardIssuer.GetBonusReward()}";
                _videoViewingButton.interactable = true;
            }
        }       
    }

    public void OnWatchVideo()
    {
        _videoViewingButton.interactable = false;
        _adsViewer.ShowRewardAd(PassingLevel);
    }

    public void OnContinue()
    {
        if (_rewardIssuer.IsRewardIssued == false)
            _rewardIssuer.PayReward();

        gameObject.SetActive(false);
        _adsViewer.ShowFullScreenAd();
        _pause.Disable();
    }

    private void OnAddBonusReward(RewardType reward)
    {
        if (reward != PassingLevel)
            return;

        _rewardIssuer.PayBonusReward();
        _wonCoinsText.text = _rewardIssuer.GetMaxReward().ToString();
    }
}
