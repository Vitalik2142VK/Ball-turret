using System;
using UnityEngine;

namespace MainMenuSpace
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerConfigurator _playerConfigurator;
        [SerializeField] private LevelPlannerConfigurator _levelsPlannerConfigurator;
        [SerializeField] private ShopConfigurator _shopConfigurator;
        [SerializeField] private UIConfigurator _userInterfaseConfigurator;

        private AdsViewer _adsViewer;

        private void OnValidate()
        {
            if (_playerConfigurator == null)
                throw new NullReferenceException(nameof(_playerConfigurator));

            if (_levelsPlannerConfigurator == null)
                throw new NullReferenceException(nameof(_levelsPlannerConfigurator));

            if (_shopConfigurator == null)
                throw new NullReferenceException(nameof(_shopConfigurator));

            if (_userInterfaseConfigurator == null)
                throw new NullReferenceException(nameof(_userInterfaseConfigurator));
        }

        private void Awake()
        {
            _adsViewer = FindAnyObjectByType<AdsViewer>();

            if (_adsViewer == null)
                throw new NullReferenceException(nameof(_adsViewer));
        }

        private void Start()
        {
            //todo Remove ConfigureWithConsol() on realise
#if UNITY_EDITOR
            Configure();
#else
            ConfigureWithConsol();
#endif
        }

        private void Configure()
        {
            _playerConfigurator.Configure(_adsViewer);
            var player = _playerConfigurator.Player;
            var playerSaver = _playerConfigurator.PlayerSaver;
            var turretImprover = _playerConfigurator.TurretImprover;
            var coinAdder = _playerConfigurator.CoinAdder;

            _levelsPlannerConfigurator.Configure(player, coinAdder);
            _shopConfigurator.Configure(playerSaver, player, turretImprover);

            var levelFactory = _levelsPlannerConfigurator.LevelFactory;
            var improvementShop = _shopConfigurator.ImprovementShop;
            var coinCountRandomizer = _levelsPlannerConfigurator.CoinCountRandomizer;

            _userInterfaseConfigurator.SetAdsViewer(_adsViewer);
            _userInterfaseConfigurator.SetImprovementShop(improvementShop);
            _userInterfaseConfigurator.Configure(player, coinAdder, levelFactory, coinCountRandomizer);

            if (player.AchievedLevelIndex == 0)
                _levelsPlannerConfigurator.LoadLearningLevel();
        }

        private void ConfigureWithConsol()
        {
            try
            {
                Console.GetLog("UNITY_WEBGL");

                Configure();
            }
            catch (Exception ex)
            {
                Console.GetException(ex);
            }
        }
    }
}
