using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AdsViewButton : MonoBehaviour
{
    private IAdsViewer _adsViewer;
    private Button _button;
    private string _rewardId;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnShowAdsView);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnShowAdsView);
    }

    public void Initialize(IAdsViewer adsViewer, string rewardId)
    {
        _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
        _rewardId = rewardId ?? throw new ArgumentNullException(nameof(rewardId));
    }

    private void OnShowAdsView()
    {
        if (_rewardId == null)
            throw new InvalidOperationException($"{nameof(_rewardId)} cannot be null");

        _adsViewer.ShowRewardAd(_rewardId);
    }
}
