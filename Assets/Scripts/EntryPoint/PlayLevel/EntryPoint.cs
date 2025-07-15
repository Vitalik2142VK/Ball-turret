using System;
using UnityEngine;
using Scriptable;

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
        [SerializeField] private ImprovedHealthConfigurator _improvedHealthConfigurator;
        [SerializeField] private BonusesPrefabConfigurator _bonusPrefabConfigurator;
        [SerializeField] private BulletConfigurator _bulletConfigurator;
        [SerializeField] private UIConfigurator _userInterfaceConfigurator;

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

            if (_improvedHealthConfigurator == null)
                throw new NullReferenceException(nameof(_improvedHealthConfigurator));

            if (_bonusPrefabConfigurator == null)
                throw new NullReferenceException(nameof(_bonusPrefabConfigurator));

            if (_bulletConfigurator == null)
                throw new NullReferenceException(nameof(_bulletConfigurator));

            if (_userInterfaceConfigurator == null)
                throw new NullReferenceException(nameof(_userInterfaceConfigurator));
        }

        private void Start()
        {
            //todo Remove try/catch in Realise
            //try
            //{

            //}
            //catch (SystemException ex)
            //{
            //    Console.GetException(ex);
            //}

            _bulletConfigurator.Configure(_player);
            _turretConfigurator.Configure(_player, _bulletConfigurator.BulletFactory);

            var turret = _turretConfigurator.Turret;
            var winState = _turretConfigurator.WinState;

            _playerController.Initialize(turret);
            _actorsConfigurator.Configure(turret, _selectedLevel.ActorsPlanner);
            _improvedHealthConfigurator.Configure(_selectedLevel.ActorsHealthCoefficient);

            var actorsController = _actorsConfigurator.ActorsController;

            _stepSystemConfigurator.Configure(turret, winState, _playerController, actorsController);
            _bonusPrefabConfigurator.Configure(turret);

            SavesData savesData = new SavesData();
            PlayerSaver playerSaver = new PlayerSaver(_player, savesData);
            RewardIssuer rewardIssuer = new RewardIssuer(playerSaver, _player, _selectedLevel, winState);
            var closeSceneStep = _stepSystemConfigurator.CloseSceneStep;
            _userInterfaceConfigurator.Configure(closeSceneStep, rewardIssuer);
        }
    }
}
