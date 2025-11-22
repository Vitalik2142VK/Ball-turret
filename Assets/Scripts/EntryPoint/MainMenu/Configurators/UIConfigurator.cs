using System;
using UnityEngine;

namespace MainMenuSpace
{
    public class UIConfigurator : MonoBehaviour
    {
        [SerializeField] private PlayMenu _playMenu;
        [SerializeField] private SettingMenu _settingMenu;
        [SerializeField] private AudioSetting _audioSetting;
        [SerializeField] private ImprovementMenu _improvementChoiseMenu;
        [SerializeField] private GameProductWindow _updateHealthButton;
        [SerializeField] private GameProductWindow _updateDamageButton;
        [SerializeField] private AddCoinsButton _addCoinsButton;
        [SerializeField] private DisableAdsButton _disableAdsButton;

        private IImprovementShop _improvementShop;
        private IAdsViewer _adsViewer;

        private void OnValidate()
        {
            if (_playMenu == null)
                throw new NullReferenceException(nameof(_playMenu));

            if (_improvementChoiseMenu == null)
                throw new NullReferenceException(nameof(_improvementChoiseMenu));

            if (_updateHealthButton == null)
                throw new NullReferenceException(nameof(_updateHealthButton));

            if (_updateDamageButton == null)
                throw new NullReferenceException(nameof(_updateDamageButton));

            if (_addCoinsButton == null)
                throw new NullReferenceException(nameof(_addCoinsButton));

            if (_settingMenu == null)
                throw new NullReferenceException(nameof(_settingMenu));

            if (_audioSetting == null)
                throw new NullReferenceException(nameof(_audioSetting));

            if (_disableAdsButton == null)
                throw new NullReferenceException(nameof(_disableAdsButton));
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
            _addCoinsButton.Initialize(coinAdder, _adsViewer);

            var adsViewButton = _addCoinsButton.GetComponent<AdsViewButton>();
            adsViewButton.Initialize(_adsViewer, RewardTypes.AddCoin);

            InitializeImprovementChoiseButtons(coinAdder);
        }

        public void InitializeImprovementChoiseButtons(ICoinAdder coinAdder)
        {
            var transaction = _improvementShop.GetTransaction(typeof(HealthImprovementTransaction));
            var product = _improvementShop.GetProduct(typeof(HealthImprovementProduct));
            float priceCoefficient = 0.4f;
            PurchaseRewardService rewardService = new PurchaseRewardService(coinAdder, priceCoefficient);

            _updateHealthButton.Initialize(transaction, product);

            transaction = _improvementShop.GetTransaction(typeof(DamageImprovementTransaction));
            product = _improvementShop.GetProduct(typeof(DamageImprovementProduct));

            _updateDamageButton.Initialize(transaction, product);
        }
    }
}
