using CannonTurret.AudioSystem;
using CannonTurret.Coin.Products;
using CannonTurret.Coin.Shops;
using CannonTurret.Coin.Transactions;
using CannonTurret.Coin.Wallets;
using CannonTurret.LevelSystem;
using CannonTurret.PlayerSystem;
using CannonTurret.SDK.Ads;
using CannonTurret.UI;
using CannonTurret.UI.MainMenu;
using System;
using UnityEngine;

namespace CannonTurret.EntryPoints.MainMenu.Configurators
{
    public class UIConfigurator : MonoBehaviour
    {
        [SerializeField] private PlayMenu _playMenu;
        [SerializeField] private SettingMenu _settingMenu;
        [SerializeField] private AudioSetting _audioSetting;
        [SerializeField] private ImprovementMenu _improvementChoiseMenu;
        [SerializeField] private GameProductWindow _updateHealthWindow;
        [SerializeField] private GameProductWindow _updateDamageWindow;
        [SerializeField] private DisableAdsButton _disableAdsButton;
        [SerializeField] private AddCoinsButton[] _addCoinsButtons;

        private IImprovementShop _improvementShop;
        private IAdsViewer _adsViewer;

        private void OnValidate()
        {
            if (_playMenu == null)
                throw new NullReferenceException(nameof(_playMenu));

            if (_improvementChoiseMenu == null)
                throw new NullReferenceException(nameof(_improvementChoiseMenu));

            if (_updateHealthWindow == null)
                throw new NullReferenceException(nameof(_updateHealthWindow));

            if (_updateDamageWindow == null)
                throw new NullReferenceException(nameof(_updateDamageWindow));

            if (_settingMenu == null)
                throw new NullReferenceException(nameof(_settingMenu));

            if (_audioSetting == null)
                throw new NullReferenceException(nameof(_audioSetting));

            if (_disableAdsButton == null)
                throw new NullReferenceException(nameof(_disableAdsButton));

            if (_addCoinsButtons == null || _addCoinsButtons.Length == 0)
                throw new InvalidOperationException(nameof(_addCoinsButtons));

            foreach (var button in _addCoinsButtons)
                if (button == null)
                    throw new NullReferenceException($"{_addCoinsButtons} contains null objects");
        }

        public void SetImprovementShop(IImprovementShop improvementShop)
        {
            _improvementShop = improvementShop ?? throw new ArgumentNullException(nameof(improvementShop));
        }

        public void SetAdsViewer(IAdsViewer adsViewer)
        {
            _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
        }

        public void Configure(IPlayer player, ICoinAdder coinAdder, ILevelFactory levelFactory, ICoinCountRandomizer coinCountRandomizer)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (coinAdder == null)
                throw new ArgumentNullException(nameof(coinAdder));

            if (levelFactory == null)
                throw new ArgumentNullException(nameof(levelFactory));

            if (coinCountRandomizer == null)
                throw new ArgumentNullException(nameof(coinCountRandomizer));

            _playMenu.Initialize(player, levelFactory);
            _settingMenu.Initialize(_audioSetting);
            _improvementChoiseMenu.Initialize(_improvementShop, _adsViewer);
            _disableAdsButton.Initialize(player.PurchasesStorage);

            foreach (var button in _addCoinsButtons)
                button.Initialize(coinAdder, _adsViewer, RewardTypes.AddCoin);

            InitializeImprovementChoiseButtons(coinAdder);
        }

        public void InitializeImprovementChoiseButtons(ICoinAdder coinAdder)
        {
            var transaction = _improvementShop.GetTransaction(typeof(HealthImprovementTransaction));
            var product = _improvementShop.GetProduct(typeof(HealthImprovementProduct));
            float priceCoefficient = 0.4f;
            PurchaseRewardService rewardService = new PurchaseRewardService(coinAdder, priceCoefficient);

            _updateHealthWindow.Initialize(transaction, product, rewardService);

            transaction = _improvementShop.GetTransaction(typeof(DamageImprovementTransaction));
            product = _improvementShop.GetProduct(typeof(DamageImprovementProduct));
            rewardService = new PurchaseRewardService(coinAdder, priceCoefficient);

            _updateDamageWindow.Initialize(transaction, product, rewardService);
        }
    }
}
