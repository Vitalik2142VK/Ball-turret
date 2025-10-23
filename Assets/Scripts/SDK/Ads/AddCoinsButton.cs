using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AddCoinsButton : MonoBehaviour
{
    private readonly string RewardId = RewardTypes.AddCoin;

    [SerializeField] private TextMeshProUGUI _addCoinsText;

    private IPlayerSaver _playerSaver;
    private IWallet _wallet;
    private IAdsViewer _adsViewer;
    private ICoinCountRandomizer _coinCountRandomizer;
    private Button _button;

    public event Action VideoViewed;

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
        if (_adsViewer != null)
        {
            _adsViewer.RewardAdViewed += OnAddCoins;
            _adsViewer.TimerRewardAdReseted += OnUpdateInteractable;
        }

        _button.onClick.AddListener(OnWatchVideo);

        if (_coinCountRandomizer != null)
            _addCoinsText.text = _coinCountRandomizer.CountCoinsForRewardAd.ToString();
    }

    private void Start()
    {
        if (_adsViewer == null)
            return;

        _button.interactable = _adsViewer.CanShowRewardAd;
    }

    private void OnDisable()
    {
        if (_adsViewer != null)
        {
            _adsViewer.RewardAdViewed -= OnAddCoins;
            _adsViewer.TimerRewardAdReseted -= OnUpdateInteractable;
        }

        _button.onClick.RemoveListener(OnWatchVideo);
    }

    public void Initialize(IPlayerSaver playerSaver, IWallet wallet, IAdsViewer adsViewer, ICoinCountRandomizer coinCountRandomizer)
    {
        _playerSaver = playerSaver ?? throw new ArgumentNullException(nameof(playerSaver));
        _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
        _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
        _coinCountRandomizer = coinCountRandomizer ?? throw new ArgumentNullException(nameof(coinCountRandomizer));
    }

    private void OnWatchVideo()
    {
        _button.interactable = false;
        _adsViewer.ShowRewardAd(RewardId);
    }

    private void OnAddCoins(string rewardId)
    {
        if (rewardId != RewardTypes.AddCoin)
            return;

        int countCoins = _coinCountRandomizer.CountCoinsForRewardAd;
        _wallet.AddCoins(countCoins);
        _playerSaver.Save();

        VideoViewed?.Invoke();
    }

    private void OnUpdateInteractable()
    {
        _button.interactable = _adsViewer.CanShowRewardAd;
    }
}
