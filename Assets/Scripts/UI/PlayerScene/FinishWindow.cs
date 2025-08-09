using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishWindow : MonoBehaviour, IMenu
{
    private readonly string AddCoinRewardId = RewardTypes.AddCoin;

    [SerializeField] private Pause _pause;
    [SerializeField] private Button _videoViewingButton;
    [SerializeField] private TextMeshProUGUI _wonBonusCoinsText;
    [SerializeField] private TextMeshProUGUI _wonCoinsText;
    [SerializeField] private Image _winBord;
    [SerializeField] private Image _defeatBord;

    private IRewardIssuer _rewardIssuer;
    private IAdsViewer _adsViewer;
    private IWinStatus _winStatus;

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

        if (_winBord == null)
            throw new ArgumentNullException(nameof(_winBord));

        if (_defeatBord == null)
            throw new ArgumentNullException(nameof(_defeatBord));
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        _videoViewingButton.interactable = false;
        _winBord.gameObject.SetActive(false);
        _defeatBord.gameObject.SetActive(false);
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

    public void Initialize(IRewardIssuer rewardIssuer, IAdsViewer adsViewer, IWinStatus winStatus)
    {
        _rewardIssuer = rewardIssuer ?? throw new ArgumentNullException(nameof(rewardIssuer));
        _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
        _winStatus = winStatus ?? throw new ArgumentNullException(nameof(winStatus));
    }

    public void Enable()
    {
        _pause.Enable();
        gameObject.SetActive(true);

        if (_winStatus.IsWin)
            _winBord.gameObject.SetActive(true);
        else
            _defeatBord.gameObject.SetActive(true);

        if (_adsViewer.IsAdsDisable || _rewardIssuer.GetReward() == 0)
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
        _adsViewer.ShowRewardAd(AddCoinRewardId);
    }

    public void OnContinue()
    {
        if (_rewardIssuer.IsRewardIssued == false)
            _rewardIssuer.PayReward();

        gameObject.SetActive(false);
        _adsViewer.ShowFullScreenAd();
        _pause.Disable();
    }

    private void OnAddBonusReward(string rewardId)
    {
        if (rewardId != AddCoinRewardId)
            return;

        _rewardIssuer.PayBonusReward();
        _wonCoinsText.text = _rewardIssuer.GetMaxReward().ToString();
    }
}
