using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IAnimatorUI), typeof(HiderUI))]
public class FinishWindow : MonoBehaviour, IWindow
{
    [SerializeField] private ScaleAnimatorUI _videoViewingButton;
    [SerializeField] private TextMeshProUGUI _wonCoinsText;
    [SerializeField] private Image _winBord;
    [SerializeField] private Image _defeatBord;
    [SerializeField] private Button[] _finishButtons;

    private IAnimatorUI _animator;
    private IRewardData _rewardData;
    private IAdsViewer _adsViewer;
    private IWinStatus _winStatus;
    private HiderUI _hiderUI;

    private void OnValidate()
    {
        if (_videoViewingButton == null)
            throw new ArgumentNullException(nameof(_videoViewingButton));

        if (_wonCoinsText == null)
            throw new ArgumentNullException(nameof(_wonCoinsText));

        if (_winBord == null)
            throw new ArgumentNullException(nameof(_winBord));

        if (_defeatBord == null)
            throw new ArgumentNullException(nameof(_defeatBord));

        if (_finishButtons == null || _finishButtons.Length == 0)
            throw new InvalidOperationException(nameof(_finishButtons));

        foreach (var button in _finishButtons)
            if (button == null)
                throw new NullReferenceException($"{_finishButtons} contains null objects");
    }

    private void Awake()
    {
        _animator = GetComponent<IAnimatorUI>();
        _hiderUI = GetComponent<HiderUI>();

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (_adsViewer != null)
            _adsViewer.ShowCompleted += OnRefreshBonusRewardData;

        foreach (var button in _finishButtons)
            button.onClick.AddListener(OnContinue);
    }

    private void OnDisable()
    {
        if (_adsViewer != null)
            _adsViewer.ShowCompleted -= OnRefreshBonusRewardData;

        foreach (var button in _finishButtons)
            button.onClick.RemoveListener(OnContinue);
    }

    public void Initialize(IRewardData rewardData, IAdsViewer adsViewer, IWinStatus winStatus)
    {
        _rewardData = rewardData ?? throw new ArgumentNullException(nameof(rewardData));
        _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
        _winStatus = winStatus ?? throw new ArgumentNullException(nameof(winStatus));
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _animator.Show();
        _hiderUI.Hide();

        EnableWinBord(_winStatus.IsWin);

        if (_adsViewer.IsAdsDisable || _rewardData.Reward == 0)
        {
            _wonCoinsText.text = _rewardData.MaxReward.ToString();
            _videoViewingButton.gameObject.SetActive(false);
        }
        else
        {
            _wonCoinsText.text = _rewardData.Reward.ToString();

            if (_adsViewer.CanShowRewardAd && _adsViewer.IsAdsDisable == false)
                _videoViewingButton.gameObject.SetActive(true);
        }
    }

    private void OnContinue()
    {
        gameObject.SetActive(false);
        _adsViewer.ShowFullScreenAd();
    }

    private void OnRefreshBonusRewardData(bool hasAdsViewedEnd)
    {
        if (hasAdsViewedEnd == false)
            return;

        _videoViewingButton.Hide();
        _wonCoinsText.text = _rewardData.MaxReward.ToString();

        StartCoroutine(WaitClosureButton());
    }

    private IEnumerator WaitClosureButton()
    {
        yield return _videoViewingButton.GetYieldAnimation();

        _videoViewingButton.gameObject.SetActive(false);
    }

    private void EnableWinBord(bool isEnable)
    {
        _winBord.gameObject.SetActive(isEnable);
        _defeatBord.gameObject.SetActive(isEnable == false);
    }
}
