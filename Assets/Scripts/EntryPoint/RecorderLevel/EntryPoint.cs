using System;
using UnityEngine;

namespace RecorderLevel
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private AIPlayerController _playerController;
        [SerializeField] private TurretConfigurator _turretConfigurator;
        [SerializeField] private EnemiesConfigurator _actorsConfigurator;
        [SerializeField] private StepSystemConfigurator _stepSystemConfigurator;

        private void OnValidate()
        {
            if (_playerController == null)
                throw new NullReferenceException(nameof(_playerController));

            if (_stepSystemConfigurator == null)
                throw new NullReferenceException(nameof(_stepSystemConfigurator));

            if (_actorsConfigurator == null)
                throw new NullReferenceException(nameof(_actorsConfigurator));

            if (_turretConfigurator == null)
                throw new NullReferenceException(nameof(_turretConfigurator));
        }

        private void Start()
        {
            Configure();
        }

        private void Configure()
        {
            _turretConfigurator.Configure();

            var turret = _turretConfigurator.Turret;

            _playerController.Initialize(turret);

            _actorsConfigurator.Configure();
            _stepSystemConfigurator.Configure(_playerController, turret, _actorsConfigurator.ActorsMover);
        }
    }
}
