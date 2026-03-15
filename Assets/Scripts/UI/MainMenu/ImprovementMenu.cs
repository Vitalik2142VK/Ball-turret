using CannonTurret.Coin.Shops;
using CannonTurret.Coin.Transactions;
using CannonTurret.SDK.Ads;
using CannonTurret.UI.Animations;
using System;
using System.Collections;
using UnityEngine;

namespace CannonTurret.UI.MainMenu
{
    [RequireComponent(typeof(ShiftAnimatorUI))]
    public class ImprovementMenu : MonoBehaviour
    {
        private const int MaxCountProductWindows = 2;

        [SerializeField] private GameProductWindow[] _gameProductWindows;

        private IWindow _previousWindow;
        private IImprovementShop _improvementShop;
        private IAdsViewer _adsViewer;
        private IAnimatorUI _animator;

        private void OnValidate()
        {
            if (_gameProductWindows == null || _gameProductWindows.Length != MaxCountProductWindows)
                _gameProductWindows = new GameProductWindow[MaxCountProductWindows];

            foreach (var window in _gameProductWindows)
                if (window == null)
                    throw new NullReferenceException($"{_gameProductWindows} contains null objects");
        }

        private void Awake()
        {
            _animator = GetComponent<IAnimatorUI>();

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            foreach (var window in _gameProductWindows)
                window.Selected += OnImprove;

            if (_adsViewer != null)
            {
                _adsViewer.ShowCompleted += OnHadleShowAds;
                _adsViewer.TimerRewardAdReseted += OnUpdateData;
            }
        }

        private void OnDisable()
        {
            foreach (var window in _gameProductWindows)
                window.Selected -= OnImprove;

            if (_adsViewer != null)
            {
                _adsViewer.ShowCompleted -= OnHadleShowAds;
                _adsViewer.TimerRewardAdReseted -= OnUpdateData;
            }
        }

        public void Initialize(IImprovementShop improvementShop, IAdsViewer adsViewer)
        {
            _improvementShop = improvementShop ?? throw new ArgumentNullException(nameof(improvementShop));
            _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
        }

        public void Open(IWindow previousWindow)
        {
            _previousWindow = previousWindow ?? throw new ArgumentNullException(nameof(previousWindow));

            gameObject.SetActive(true);
            _adsViewer.ShowFullScreenAd();
            _animator.Show();

            StartCoroutine(WaitOpening());
        }

        public void OnClose()
        {
            _animator.Hide();

            StartCoroutine(WaitClosure());
        }

        private void OnImprove(IGamePayTransaction gamePayTransaction)
        {
            if (gamePayTransaction == null)
                throw new ArgumentNullException(nameof(gamePayTransaction));

            if (_improvementShop.TryMakeTransaction(gamePayTransaction))
                OnUpdateData();
            else
                throw new InvalidOperationException("The transaction failed");
        }

        private void OnUpdateData()
        {
            foreach (var window in _gameProductWindows)
                window.UpdateData();
        }

        private void OnHadleShowAds(bool hasAdsViewedEnd)
        {
            foreach (var window in _gameProductWindows)
            {
                window.HandleReservation(hasAdsViewedEnd);
                window.UpdateData();
            }
        }

        private IEnumerator WaitOpening()
        {
            yield return _animator.GetYieldAnimation();

            OnUpdateData();
        }

        private IEnumerator WaitClosure()
        {
            yield return _animator.GetYieldAnimation();

            gameObject.SetActive(false);
            _previousWindow.Enable();
        }
    }
}