using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IAnimatorUI))]
public class FinishWindow : MonoBehaviour, IWindow
{
    [SerializeField] private Pause _pause;
    [SerializeField] private Button _finishButton;
    [SerializeField] private ScaleAnimatorUI _videoViewingButton;
    [SerializeField] private TextMeshProUGUI _wonCoinsText;
    [SerializeField] private Image _winBord;
    [SerializeField] private Image _defeatBord;

    private IAnimatorUI _animator;
    private IRewardIssuer _rewardIssuer;
    private IAdsViewer _adsViewer;
    private IWinStatus _winStatus;
    
    private void OnValidate()
    {
        if (_pause == null)
            throw new ArgumentNullException(nameof(_pause));

        if (_finishButton == null)
            throw new ArgumentNullException(nameof(_finishButton));

        if (_videoViewingButton == null)
            throw new ArgumentNullException(nameof(_videoViewingButton));

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
        _winBord.gameObject.SetActive(false);
        _defeatBord.gameObject.SetActive(false);
        _animator = GetComponent<IAnimatorUI>();
    }

    private void OnEnable()
    {
        if (_adsViewer != null)
            _adsViewer.ShowCompleted += OnAddBonusReward;

        _finishButton.onClick.AddListener(OnContinue);
    }

    private void OnDisable()
    {
        if (_adsViewer != null)
            _adsViewer.ShowCompleted -= OnAddBonusReward;

        _finishButton.onClick.RemoveListener(OnContinue);
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
        _animator.Show();

        if (_winStatus.IsWin)
            _winBord.gameObject.SetActive(true);
        else
            _defeatBord.gameObject.SetActive(true);

        if (_adsViewer.IsAdsDisable || _rewardIssuer.GetReward() == 0)
        {
            _wonCoinsText.text = _rewardIssuer.GetMaxReward().ToString();
            _rewardIssuer.PayMaxReward();
            _videoViewingButton.gameObject.SetActive(false);
        }
        else
        {
            _wonCoinsText.text = _rewardIssuer.GetReward().ToString();
            _rewardIssuer.PayReward();

            if (_adsViewer.CanShowRewardAd && _adsViewer.IsAdsDisable == false)
                _videoViewingButton.gameObject.SetActive(true);
        }       
    }

    private void OnContinue()
    {
        if (_rewardIssuer.IsRewardIssued == false)
            _rewardIssuer.PayReward();

        gameObject.SetActive(false);
        _adsViewer.ShowFullScreenAd();
        _pause.Disable();
    }

    private void OnAddBonusReward()
    {
        _videoViewingButton.Hide();
        _wonCoinsText.text = _rewardIssuer.GetMaxReward().ToString();

        StartCoroutine(WaitClosureButton());
    }

    private IEnumerator WaitClosureButton()
    {
        yield return _videoViewingButton.GetYieldAnimation();

        _videoViewingButton.gameObject.SetActive(false);
    }
}
