using Scriptable;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayLevel
{
    public partial class EntryPoint : MonoBehaviour
    {
        [SerializeField] private SelectedLevel _selectedLevel;
        [SerializeField] private CachedPlayer _player;
        [SerializeField, SerializeIterface(typeof(IPlayerController))] private GameObject _playerController;

        [Header("Configurators")]
        [SerializeField] private TurretConfigurator _turretConfigurator;
        [SerializeField] private StepControllerConfigurator _stepControllerConfigurator;
        [SerializeField] private ActorsConfigurator _actorsConfigurator;
        [SerializeField] private BonusesConfigurator _bonusPrefabConfigurator;
        [SerializeField] private BulletConfigurator _bulletConfigurator;
        [SerializeField] private UIConfigurator _userInterfaceConfigurator;
        [SerializeField] private FinishWindowConfigurator _finishWindowConfigurator;
        [SerializeField] private BonusesWindowHiderConfigurator _bonusesWindowHiderConfigurator;

        private AdsViewer _adsViewer;
        private CoinAdder _coinsAdder;

        public Config Configs { get; private set; }

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

            if (_stepControllerConfigurator == null)
                throw new NullReferenceException(nameof(_stepControllerConfigurator));

            if (_actorsConfigurator == null)
                throw new NullReferenceException(nameof(_actorsConfigurator));

            if (_bonusPrefabConfigurator == null)
                throw new NullReferenceException(nameof(_bonusPrefabConfigurator));

            if (_bulletConfigurator == null)
                throw new NullReferenceException(nameof(_bulletConfigurator));

            if (_userInterfaceConfigurator == null)
                throw new NullReferenceException(nameof(_userInterfaceConfigurator));

            if (_finishWindowConfigurator == null)
                throw new NullReferenceException(nameof(_finishWindowConfigurator));

            if (_bonusesWindowHiderConfigurator == null)
                throw new NullReferenceException(nameof(_bonusesWindowHiderConfigurator));
        }

        private void Start()
        {
            Configure();
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

            IPlayerController playerController = _playerController.GetComponent<IPlayerController>();

            _bulletConfigurator.Configure(_player);
            _turretConfigurator.Configure(_player, _bulletConfigurator.BulletFactory);

            var turret = _turretConfigurator.Turret;
            var shooterView = _turretConfigurator.ShooterView;
            SavedPlayerData savesData = new SavedPlayerData();
            PlayerSaver playerSaver = new PlayerSaver(_player, savesData);
            _coinsAdder = new CoinAdder(playerSaver, _player.Wallet, _adsViewer);
            RewardIssuer rewardIssuer = new RewardIssuer(_coinsAdder, _player, _selectedLevel);
            WinStatus winStatus = new WinStatus(turret, _selectedLevel);
            LevelStatus levelStatus = new LevelStatus(turret, _selectedLevel);

            playerController.Initialize(turret);
            _actorsConfigurator.Configure(turret, _selectedLevel, winStatus);

            var actorsControllersAccess = _actorsConfigurator.ControllersAccess;
            var enemiesController = actorsControllersAccess.EnemiesController;
            VictoryController victoryController = new VictoryController(enemiesController, shooterView, winStatus);
            DataForStepSystem dataForStepSystem = new DataForStepSystem(turret, _adsViewer, rewardIssuer, playerController, victoryController, actorsControllersAccess, levelStatus);

            _stepControllerConfigurator.Configure(dataForStepSystem);
            _bonusPrefabConfigurator.Configure(enemiesController);
            _stepControllerConfigurator.ConfigureBonusActivationStep(_bonusPrefabConfigurator.BonusReservator);

            var changeSceneStep = _stepControllerConfigurator.ChangeSceneStep;

            _userInterfaceConfigurator.Configure(changeSceneStep, _selectedLevel);
            _finishWindowConfigurator.Configure(_coinsAdder, rewardIssuer, _adsViewer, winStatus, changeSceneStep, _selectedLevel);
            _bonusesWindowHiderConfigurator.Configure(_turretConfigurator.ShotAction);

            Configs = new Config(_stepControllerConfigurator, _actorsConfigurator, _userInterfaceConfigurator, _finishWindowConfigurator, winStatus);

            if (_player.AchievedLevelIndex == 0)
                SceneManager.LoadScene((int)SceneIndex.LearningScene, LoadSceneMode.Additive);
        }
    }
}
