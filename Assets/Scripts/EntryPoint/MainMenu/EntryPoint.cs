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

        private void Start()
        {
            _playerConfigurator.Configure();

            var player = _playerConfigurator.Player;
            var turretImprover = _playerConfigurator.TurretImprover;

            _levelsPlannerConfigurator.Configure(player);
            _shopConfigurator.Configure(player, turretImprover);

            var levelFactory = _levelsPlannerConfigurator.LevelFactory;
            var coinCountRandomizer = _levelsPlannerConfigurator.CoinCountRandomizer;
            var improvementShop = _shopConfigurator.ImprovementShop;

            _userInterfaseConfigurator.Configure(player, levelFactory, improvementShop, coinCountRandomizer);
        }
    }
}
