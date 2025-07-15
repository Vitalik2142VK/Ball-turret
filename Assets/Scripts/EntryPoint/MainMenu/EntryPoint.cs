using System;
using UnityEngine;
using YG;

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
            //todo Remove try/catch in Relise
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    Console.GetException(ex);
            //}

            _playerConfigurator.Configure();

            var player = _playerConfigurator.Player;
            var playerSaver = _playerConfigurator.PlayerSaver;
            var turretImprover = _playerConfigurator.TurretImprover;

            _levelsPlannerConfigurator.Configure(player);
            _shopConfigurator.Configure(playerSaver, player, turretImprover);

            var levelFactory = _levelsPlannerConfigurator.LevelFactory;
            var improvementShop = _shopConfigurator.ImprovementShop;
            var coinCountRandomizer = _levelsPlannerConfigurator.CoinCountRandomizer;

            _userInterfaseConfigurator.SetImprovementShop(improvementShop);
            _userInterfaseConfigurator.Configure(playerSaver, player, levelFactory, coinCountRandomizer);
        }
    }
}
