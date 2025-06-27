using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AddCoinsButton : MonoBehaviour
{
    private const RewardType AddCoin = RewardType.AddCoin;

    [SerializeField] private TextMeshProUGUI _addCoinsText;
    [SerializeField, Min(0.02f)] private float _timeWaitUpdate = 0.5f;

    private IPlayerSaver _playerSaver;
    private IWallet _wallet;
    private IAdsViewer _adsViewer;
    private ICoinCountRandomizer _coinCountRandomizer;
    private WaitForSeconds _waitUpdateEnable;
    private Button _videoViewingButton;
    private Coroutine _videoViewingCoroutine;

    public event Action VideoViewed;

    private void OnValidate()
    {
        if (_addCoinsText == null)
            throw new ArgumentNullException(nameof(_addCoinsText));
    }

    private void Awake()
    {
        _videoViewingButton = GetComponent<Button>();
        _videoViewingButton.interactable = false;

        _waitUpdateEnable = new WaitForSeconds(_timeWaitUpdate);
    }

    private void OnEnable()
    {
        if (_adsViewer != null)
            _adsViewer.RewardAdViewed += OnAddCoins;

        if (_coinCountRandomizer != null)
            _addCoinsText.text = _coinCountRandomizer.CountCoinsForRewardAd.ToString();
    }

    private void Start()
    {
        if (_adsViewer == null)
            return;

        _videoViewingButton.interactable = _adsViewer.CanShowRewardAd;

        if (_adsViewer.CanShowRewardAd == false)
            _videoViewingCoroutine = StartCoroutine(UpdateInteractable());
    }

    private void OnDisable()
    {
        if (_adsViewer != null)
            _adsViewer.RewardAdViewed -= OnAddCoins;

        if (_videoViewingCoroutine != null)
            StopCoroutine(_videoViewingCoroutine);
    }

    public void Initialize(IPlayerSaver playerSaver, IWallet wallet, IAdsViewer adsViewer, ICoinCountRandomizer coinCountRandomizer)
    {
        _playerSaver = playerSaver ?? throw new ArgumentNullException(nameof(playerSaver));
        _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
        _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
        _coinCountRandomizer = coinCountRandomizer ?? throw new ArgumentNullException(nameof(coinCountRandomizer));
    }

    public void OnWatchVideo()
    {
        _videoViewingButton.interactable = false;
        _adsViewer.ShowRewardAd(AddCoin);
    }

    private void OnAddCoins(RewardType reward)
    {
        if (reward != AddCoin)
            return;

        int countCoins = _coinCountRandomizer.CountCoinsForRewardAd;
        _wallet.AddCoins(countCoins);
        _playerSaver.Save();
        _videoViewingCoroutine = StartCoroutine(UpdateInteractable());

        VideoViewed?.Invoke();
    }

    private IEnumerator UpdateInteractable()
    {
        while (_adsViewer.CanShowRewardAd == false)
            yield return _waitUpdateEnable;

        _videoViewingButton.interactable = _adsViewer.CanShowRewardAd;
    }
}
