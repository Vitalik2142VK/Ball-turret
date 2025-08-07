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
        [SerializeField] private ImprovementChoiseButton _updateHealthButton;
        [SerializeField] private ImprovementChoiseButton _updateDamageButton;
        [SerializeField] private AddCoinsButton _addCoinsButton;
        [SerializeField] private DisableAdsButton _disableAdsButton;

        private IImprovementShop _improvementShop;
        private AdsViewer _adsViewer;

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

        private void Awake()
        {
            _adsViewer = FindAnyObjectByType<AdsViewer>();

            if (_adsViewer == null)
                throw new NullReferenceException(nameof(_adsViewer));
        }

        public void SetImprovementShop(IImprovementShop improvementShop)
        {
            _improvementShop = improvementShop ?? throw new ArgumentNullException(nameof(improvementShop));
        }

        public void Configure(IPlayerSaver playerSaver, IPlayer player, ILevelFactory levelFactory, ICoinCountRandomizer coinCountRandomizer)
        {
            if (playerSaver == null)
                throw new ArgumentNullException(nameof(playerSaver));

            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (levelFactory == null)
                throw new ArgumentNullException(nameof(levelFactory));

            if (coinCountRandomizer == null)
                throw new ArgumentNullException(nameof(coinCountRandomizer));

            _playMenu.Initialize(player, levelFactory);
            _settingMenu.Initialize(_audioSetting);
            _adsViewer.Initialize(player.PurchasesStorage);
            _improvementChoiseMenu.Initialize(_improvementShop, _adsViewer);
            _addCoinsButton.Initialize(playerSaver, player.Wallet, _adsViewer, coinCountRandomizer);
            _disableAdsButton.Initialize(player.PurchasesStorage);

            InitializeImprovementChoiseButtons();
        }

        public void InitializeImprovementChoiseButtons()
        {
            var transaction = _improvementShop.GetTransaction(typeof(HealthImprovementTransaction));
            var product = _improvementShop.GetProduct(typeof(HealthImprovementProduct));

            _updateHealthButton.Initialize(transaction, product);

            transaction = _improvementShop.GetTransaction(typeof(DamageImprovementTransaction));
            product = _improvementShop.GetProduct( typeof(DamageImprovementProduct));

            _updateDamageButton.Initialize(transaction, product);
        }
    }
}
