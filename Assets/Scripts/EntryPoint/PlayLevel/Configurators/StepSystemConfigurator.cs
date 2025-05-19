using System;
using UnityEngine;

namespace PlayLevel
{
    public class StepSystemConfigurator : MonoBehaviour
    {
        [SerializeField] private StepSystem _stepSystem;
        [SerializeField] private BulletsCollector _bulletCollector;
        [SerializeField] private MainMenuLoader _mainMenuLoader;

        private ITurret _turret;
        private Player _player;
        private ActorsController _actorsController;

        private PlayerShotStep _playerShotStep;
        private BonusActivationStep _bonusActivationStep;
        private PrepareActorsStep _prepareActorsStep;
        private ActorsMoveStep _objectsMoveStep;
        private EnemyAttackStep _enemyAttackStep;
        private RemoveActorsStep _removeActorsStep;
        private FinalStep _finalStep;

        public CloseSceneStep CloseSceneStep { get; private set; }

        private void OnValidate()
        {
            if (_stepSystem == null)
                throw new NullReferenceException(nameof(_stepSystem));
        }

        private void OnDisable()
        {
            CloseSceneStep.Disable();
        }

        public void Configure(ITurret turret, Player player, ActorsController actorsController)
        {
            _player = player != null ? player : throw new NullReferenceException(nameof(player));
            _turret = turret ?? throw new NullReferenceException(nameof(turret));
            _actorsController = actorsController ?? throw new NullReferenceException(nameof(actorsController));

            CreateSteps();
            ConnectSteps();
            CreateFinalSteps();

            _stepSystem.EstablishNextStep(_finalStep);
        }

        private void CreateSteps()
        {
            _playerShotStep = new PlayerShotStep(_player);
            _bonusActivationStep = new BonusActivationStep(_bulletCollector);
            _prepareActorsStep = new PrepareActorsStep(_actorsController);
            _objectsMoveStep = new ActorsMoveStep(_actorsController);
            _enemyAttackStep = new EnemyAttackStep(_actorsController);
            _removeActorsStep = new RemoveActorsStep(_actorsController);
            CloseSceneStep = new CloseSceneStep(_mainMenuLoader, _player.User, _turret);
        }

        private void ConnectSteps()
        {
            AddNextStepToEndPoint(_turret, _bonusActivationStep);
            AddNextStepToEndPoint(_bonusActivationStep, _prepareActorsStep);
            AddNextStepToEndPoint(_prepareActorsStep, _objectsMoveStep);
            AddNextStepToEndPoint(_objectsMoveStep, _enemyAttackStep);
            AddNextStepToEndPoint(_enemyAttackStep, _removeActorsStep);
        }

        private void AddNextStepToEndPoint(IEndPointStep endPointStep, IStep nextStep)
        {
            IEndStep endStep = new NextStep(_stepSystem, nextStep);
            endPointStep.SetEndStep(endStep);
        }

        private void CreateFinalSteps()
        {
            DynamicNextStep nextStep = new DynamicNextStep(_stepSystem);
            _finalStep = new FinalStep(_actorsController, nextStep);
            _finalStep.SetStartStep(_prepareActorsStep);
            _finalStep.SetPlayerStep(_playerShotStep);
            _finalStep.SetCloseSceneStep(CloseSceneStep);

            AddNextStepToEndPoint(_removeActorsStep, _finalStep);
        }
    }
}
