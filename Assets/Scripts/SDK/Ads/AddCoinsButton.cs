using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(AdsViewButton))]
public class AddCoinsButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _addCoinsText;

    private ICoinAdder _coinAdder;
    private IAdsViewer _adsViewer;
    private Button _button;

    private void OnValidate()
    {
        if (_addCoinsText == null)
            throw new ArgumentNullException(nameof(_addCoinsText));
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.interactable = false;
    }

    private void OnEnable()
    {
        if (_coinAdder == null || _adsViewer == null)
            return;

        _addCoinsText.text = _coinAdder.CoinsCountAdsView.ToString();
        _adsViewer.TimerRewardAdReseted += OnUpdateInteractable;
        _button.onClick.AddListener(OnUpdateInteractable);
    }

    private void Start()
    {
        if (_coinAdder == null || _adsViewer == null)
            return;

        _button.interactable = _adsViewer.CanShowRewardAd;
        _addCoinsText.text = _coinAdder.CoinsCountAdsView.ToString();
    }

    private void OnDisable()
    {
        if (_coinAdder == null || _adsViewer == null)
            return;

        _adsViewer.TimerRewardAdReseted -= OnUpdateInteractable;
        _adsViewer.TimerRewardAdReseted -= OnUpdateInteractable;
        _button.onClick.RemoveListener(OnUpdateInteractable);
    }

    public void Initialize(ICoinAdder coinAdder, IAdsViewer adsViewer)
    {
        _coinAdder = coinAdder ?? throw new ArgumentNullException(nameof(coinAdder));
        _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
    }

    private void OnUpdateInteractable()
    {
        _button.interactable = _adsViewer.CanShowRewardAd;
    }
}
