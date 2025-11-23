using System;
using UnityEngine;

namespace RecorderLevel
{
    public class StepSystemConfigurator : MonoBehaviour
    {
        [SerializeField] private StepSystem _stepSystem;
        [SerializeField] private PlayerController _playerController;

        private ITurret _turret;
        private IActorsMover _actorsMover;

        private RecorderPlayerStep _playerStep;
        private ActorsMoveStep _objectsMoveStep;

        private void OnValidate()
        {
            if (_stepSystem == null)
                throw new NullReferenceException(nameof(_stepSystem));

            if (_playerController == null)
                throw new NullReferenceException(nameof(_playerController));
        }

        public void Configure(ITurret turret, IActorsMover actorsMover)
        {
            _turret = turret ?? throw new NullReferenceException(nameof(turret));
            _actorsMover = actorsMover ?? throw new NullReferenceException(nameof(actorsMover));

            CreateSteps();
            ConnectSteps();

            _stepSystem.EstablishNextStep(_objectsMoveStep);
        }

        private void CreateSteps()
        {
            _playerStep = new RecorderPlayerStep(_playerController);
            _objectsMoveStep = new ActorsMoveStep(_actorsMover);
        }

        private void AddNextStepToEndPoint(IEndPointStep endPointStep, IStep nextStep)
        {
            IEndStep endStep = new NextStep(_stepSystem, nextStep);
            endPointStep.SetEndStep(endStep);
        }

        private void ConnectSteps()
        {
            AddNextStepToEndPoint(_turret, _objectsMoveStep);
            AddNextStepToEndPoint(_objectsMoveStep, _playerStep);
        }
    }
}
