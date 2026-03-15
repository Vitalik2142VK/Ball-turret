using CannonTurret.EntryPoints.MainMenu.Configurators;
using CannonTurret.LevelSystem;
using CannonTurret.SDK.Ads;
using System;
using UnityEngine;
using YG;

namespace CannonTurret.EntryPoints.MainMenu
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
        }

        private void Start()
        {
            if (_adsViewer == null)
            {
                LoadStartScene();

                return;
            }

            Configure();
        }

        private void LoadStartScene()
        {
            StartSceneLoader sceneLoader = new StartSceneLoader();
            sceneLoader.Load();
        }

        private void Configure()
        {
            _playerConfigurator.Configure(_adsViewer);
            var player = _playerConfigurator.Player;
            var playerSaver = _playerConfigurator.PlayerSaver;
            var turretImprover = _playerConfigurator.TurretImprover;
            var coinAdder = _playerConfigurator.CoinAdder;

            _levelsPlannerConfigurator.Configure(player);
            _shopConfigurator.Configure(playerSaver, player, turretImprover);

            var levelFactory = _levelsPlannerConfigurator.LevelFactory;
            var improvementShop = _shopConfigurator.ImprovementShop;
            var coinCountRandomizer = _levelsPlannerConfigurator.CoinCountRandomizer;

            _userInterfaseConfigurator.SetAdsViewer(_adsViewer);
            _userInterfaseConfigurator.SetImprovementShop(improvementShop);
            _userInterfaseConfigurator.Configure(player, coinAdder, levelFactory, coinCountRandomizer);

            YG2.GameReadyAPI();

            if (player.AchievedLevelIndex == 0)
                _levelsPlannerConfigurator.LoadLearningLevel();
        }
    }
}
