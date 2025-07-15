using System;
using System.Collections;
using UnityEngine;
using YG;

public class AdsViewer : MonoBehaviour, IAdsViewer
{
    private static bool s_IsInitialized = false;

    [SerializeField, Range(60f, 180f)] private float _timeWaitNextFullScreenAd = 120f;
    [SerializeField, Range(10f, 60f)] private float _timeWaitNextRewardAd = 20f;

    private IPurchase _disableAdsPurchase;
    private WaitForSeconds _waitNextAdFullScreenAd;
    private WaitForSeconds _waitNextAdRewardAd;
    private bool _canShowFullScreen;

    public event Action<RewardType> RewardAdViewed;

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
        YandexGame.RewardVideoEvent += OnConfirmReward;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= OnConfirmReward;
    }

    public void Initialize(IPurchasesStorage purchasesStorage)
    {
        if (purchasesStorage == null)
            throw new ArgumentNullException(nameof(purchasesStorage));

        var purchaseId = PurchasesTypes.DisableAds;

        if (purchasesStorage.IsContainsId(purchaseId) == false)
            throw new ArgumentOutOfRangeException($"Purchase with id '{purchaseId}' not found.");

        _disableAdsPurchase = purchasesStorage.GetPurchase(purchaseId);
    }

    public void ShowFullScreenAd()
    {
        if (_canShowFullScreen && _disableAdsPurchase.IsConsumed == false)
            StartCoroutine(WaitShowFullScreen());
    }

    public void ShowRewardAd(RewardType reward)
    {
        if (CanShowRewardAd && _disableAdsPurchase.IsConsumed == false)
            StartCoroutine(WaitShowRewardAd(reward));
    }

    private void OnConfirmReward(int id)
    {
        if (Enum.IsDefined(typeof(RewardType), id) == false)
            throw new ArgumentOutOfRangeException(nameof(id));

        RewardType reward = (RewardType)id;
        RewardAdViewed?.Invoke(reward);
    }

    private IEnumerator WaitShowFullScreen()
    {
        _canShowFullScreen = false;

        YandexGame.FullscreenShow();

        yield return _waitNextAdFullScreenAd;

        _canShowFullScreen = true;
    }

    private IEnumerator WaitShowRewardAd(RewardType reward)
    {
        CanShowRewardAd = false;
        
        YandexGame.RewVideoShow((int)reward);

        yield return _waitNextAdRewardAd;

        CanShowRewardAd = true;
    }
}
