using System;
using System.Collections;
using UnityEngine;
using YG;

public class AdsViewer : MonoBehaviour, IAdsViewer
{
    private static bool s_IsInitialized = false;

    [SerializeField, Range(60f, 180f)] private float _timeWaitNextFullScreenAd = 120f;
    [SerializeField, Range(10f, 60f)] private float _timeWaitNextRewardAd = 20f;

    private IPlayerPurchase _disableAdsPurchase;
    private WaitForSeconds _waitNextAdFullScreenAd;
    private WaitForSeconds _waitNextAdRewardAd;
    private bool _canShowFullScreen;

    public event Action<string> RewardAdViewed;
    public event Action TimerRewardAdReseted;

    public bool IsAdsDisable => _disableAdsPurchase.IsPurchased;

    public bool CanShowRewardAd { get; private set; }

    private void Awake()
    {
        if (s_IsInitialized)
        {
            Destroy(gameObject);
            return;
        }

        s_IsInitialized = true;
        DontDestroyOnLoad(gameObject);

        _waitNextAdFullScreenAd = new WaitForSeconds(_timeWaitNextFullScreenAd);
        _waitNextAdRewardAd = new WaitForSeconds(_timeWaitNextRewardAd);
        _canShowFullScreen = true;

        CanShowRewardAd = true;
    }

    private void OnEnable()
    {
        YG2.onRewardAdv += OnConfirmReward;
    }

    private void OnDisable()
    {
        YG2.onRewardAdv -= OnConfirmReward;
    }

    public void Initialize(IPurchasesStorage purchasesStorage)
    {
        if (purchasesStorage == null)
            throw new ArgumentNullException(nameof(purchasesStorage));

        var purchaseId = PurchasesTypes.DisableAds;

        if (purchasesStorage.TryGetPurchase(out IPlayerPurchase purchase, purchaseId) == false)
            throw new ArgumentOutOfRangeException($"Purchase with id '{purchaseId}' not found.");

        _disableAdsPurchase = purchase;
    }

    public void ShowFullScreenAd()
    {
        if (_canShowFullScreen && _disableAdsPurchase.IsPurchased == false)
            StartCoroutine(WaitShowFullScreen());
    }

    public void ShowRewardAd(string rewardId)
    {
        if (CanShowRewardAd)
            StartCoroutine(WaitShowRewardAd(rewardId));
    }

    private void OnConfirmReward(string rewardId)
    {
        if (string.IsNullOrEmpty(rewardId))
            throw new ArgumentOutOfRangeException(nameof(rewardId));

        RewardAdViewed?.Invoke(rewardId);
    }

    private IEnumerator WaitShowFullScreen()
    {
        _canShowFullScreen = false;

        YG2.InterstitialAdvShow();

        yield return _waitNextAdFullScreenAd;

        _canShowFullScreen = true;
    }

    private IEnumerator WaitShowRewardAd(string rewardId)
    {
        CanShowRewardAd = false;

        YG2.RewardedAdvShow(rewardId);

        yield return _waitNextAdRewardAd;

        CanShowRewardAd = true;
        TimerRewardAdReseted?.Invoke();
    }
}
