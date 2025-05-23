using System;
using UnityEngine;

namespace PlayLevel
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Scriptable.SelectedLevel _selectedLevel;

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
            if (_player == null)
                throw new NullReferenceException(nameof(_player));

            if (_selectedLevel == null)
                throw new NullReferenceException(nameof(_selectedLevel));

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
            IUser user = _player.User;

            _bulletConfigurator.Configure(user);
            _turretConfigurator.Configure(user, _bulletConfigurator.BulletFactory);

            var turret = _turretConfigurator.Turret;

            _player.Initialize(turret);
            _actorsConfigurator.Configure(turret, _selectedLevel.ActorsPlanner);
            _improvedHealthConfigurator.Configure(_selectedLevel.ActorsHealthCoefficient);

            var actorsController = _actorsConfigurator.ActorsController;
            RewardIssuer rewardIssuer = new RewardIssuer(user, _selectedLevel);

            _stepSystemConfigurator.Configure(turret, rewardIssuer, _player, actorsController);
            _bonusPrefabConfigurator.Configure(turret);

            var closeSceneStep = _stepSystemConfigurator.CloseSceneStep;
            _userInterfaceConfigurator.Configure(closeSceneStep, rewardIssuer);
        }
    }
}
