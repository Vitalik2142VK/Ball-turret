using Scriptable;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayLevel
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private SelectedLevel _selectedLevel;
        [SerializeField] private CachedPlayer _player;
        [SerializeField] private PlayerController _playerController;

        [Header("Configurators")]
        [SerializeField] private TurretConfigurator _turretConfigurator;
        [SerializeField] private StepSystemConfigurator _stepSystemConfigurator;
        [SerializeField] private ActorsConfigurator _actorsConfigurator;
        [SerializeField] private BonusesConfigurator _bonusPrefabConfigurator;
        [SerializeField] private BulletConfigurator _bulletConfigurator;
        [SerializeField] private UIConfigurator _userInterfaceConfigurator;
        [SerializeField] private BonusesWindowHiderConfigurator _bonusesWindowHiderConfigurator;

        private AdsViewer _adsViewer;
        private CoinAdder _coinsAdder;

        private void OnValidate()
        {
            if (_selectedLevel == null)
                throw new NullReferenceException(nameof(_selectedLevel));

            if (_player == null)
                throw new NullReferenceException(nameof(_player));

            if (_playerController == null)
                throw new NullReferenceException(nameof(_playerController));

            if (_turretConfigurator == null)
                throw new NullReferenceException(nameof(_turretConfigurator));

            if (_stepSystemConfigurator == null)
                throw new NullReferenceException(nameof(_stepSystemConfigurator));

            if (_actorsConfigurator == null)
                throw new NullReferenceException(nameof(_actorsConfigurator));

            if (_bonusPrefabConfigurator == null)
                throw new NullReferenceException(nameof(_bonusPrefabConfigurator));

            if (_bulletConfigurator == null)
                throw new NullReferenceException(nameof(_bulletConfigurator));

            if (_userInterfaceConfigurator == null)
                throw new NullReferenceException(nameof(_userInterfaceConfigurator));

            if (_bonusesWindowHiderConfigurator == null)
                throw new NullReferenceException(nameof(_bonusesWindowHiderConfigurator));
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

        private void OnDisable()
        {
            _coinsAdder.Disable();
        }

        private void Configure()
        {
            _adsViewer = FindAnyObjectByType<AdsViewer>();

            if (_adsViewer == null)
                throw new NullReferenceException(nameof(_adsViewer));

            _bulletConfigurator.Configure(_player);
            _turretConfigurator.Configure(_player, _bulletConfigurator.BulletFactory);

            var turret = _turretConfigurator.Turret;

            _playerController.Initialize(turret);
            _actorsConfigurator.Configure(turret, _selectedLevel);

            var actorsController = _actorsConfigurator.ActorsController;

            _stepSystemConfigurator.Configure(turret, _playerController, actorsController);
            _bonusPrefabConfigurator.Configure(actorsController);
            _stepSystemConfigurator.ConfigureBonusActivationStep(_bonusPrefabConfigurator.BonusReservator);

            SavedPlayerData savesData = new SavedPlayerData();
            PlayerSaver playerSaver = new PlayerSaver(_player, savesData);
            _coinsAdder = new CoinAdder(playerSaver, _player.Wallet, _adsViewer);
            RewardIssuer rewardIssuer = new RewardIssuer(_coinsAdder, _player, _selectedLevel);
            WinStatus winStatus = new WinStatus(turret, _selectedLevel);
            var closeSceneStep = _stepSystemConfigurator.CloseSceneStep;
            _userInterfaceConfigurator.Configure(closeSceneStep, rewardIssuer, winStatus, _coinsAdder, _adsViewer);
            _bonusesWindowHiderConfigurator.Configure(_turretConfigurator.ShotAction);

            if (_player.AchievedLevelIndex == 0)
                SceneManager.LoadScene((int)SceneIndex.LearningScene, LoadSceneMode.Additive);
        }

        private void ConfigureWithConsol()
        {
            try
            {
                Configure();
            }
            catch (Exception ex)
            {
                Console.GetException(ex);
            }
        }
    }

    public class TestLevelLoader : MonoBehaviour
    {

    }
}
