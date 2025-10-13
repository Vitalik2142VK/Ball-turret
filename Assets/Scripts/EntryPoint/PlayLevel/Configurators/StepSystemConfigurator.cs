using System;
using UnityEngine;

namespace PlayLevel
{
    public class StepSystemConfigurator : MonoBehaviour
    {
        [SerializeField] private StepSystem _stepSystem;
        [SerializeField] private ComboCounter _comboCounter;
        [SerializeField] private BulletsCollector _bulletCollector;
        [SerializeField] private MainMenuLoader _mainMenuLoader;
        [SerializeField] private FinishWindow _finishWindow;
        [SerializeField] private FreezingBonusActivatorCreator _freezerCreator;

        private ITurret _turret;
        private PlayerController _playerController;
        private ActorsController _actorsController;

        private IDynamicEndStep _nextStepPrepareActors;
        private PlayerShotStep _playerShotStep;
        private ResetComboStep _resetComboStep;
        private BonusActivationStep _bonusActivationStep;
        private PrepareActorsStep _prepareActorsStep;
        private ActorsFreezeStep _actorsFreezeStep;
        private ActorsMoveStep _objectsMoveStep;
        private EnemyAttackStep _enemyAttackStep;
        private RemoveActorsStep _removeActorsStep;
        private CyclicalStep _cyclicalStep;
        private RewardStep _rewardStep;
        private CloseSceneStep _closeSceneStep;

        public IStep CloseSceneStep => _closeSceneStep;

        private void OnValidate()
        {
            if (_stepSystem == null)
                throw new NullReferenceException(nameof(_stepSystem));

            if (_comboCounter == null)
                throw new NullReferenceException(nameof(_comboCounter));

            if (_bulletCollector == null)
                throw new NullReferenceException(nameof(_bulletCollector));

            if (_mainMenuLoader == null)
                throw new NullReferenceException(nameof(_mainMenuLoader));

            if (_finishWindow == null)
                throw new NullReferenceException(nameof(_finishWindow));

            if (_freezerCreator == null)
                throw new NullReferenceException(nameof(_freezerCreator));
        }

        public void Configure(ITurret turret, PlayerController playerController, ActorsController actorsController)
        {
            _playerController = playerController != null ? playerController : throw new NullReferenceException(nameof(playerController));
            _turret = turret ?? throw new NullReferenceException(nameof(turret));
            _actorsController = actorsController ?? throw new NullReferenceException(nameof(actorsController));

            CreateSteps();
            CreateFinalSteps();
            ConnectSteps();
            CreateDynamicNextStepPrepareActors();

            _stepSystem.EstablishNextStep(_cyclicalStep);

            _actorsFreezeStep = new ActorsFreezeStep(_nextStepPrepareActors, _objectsMoveStep);
            AddNextStepToEndPoint(_actorsFreezeStep, _removeActorsStep);
            _freezerCreator.Initialize(_nextStepPrepareActors, _actorsFreezeStep);
        }

        public void AddLearningStep(LearningStep learningStep)
        {
            if (learningStep == null)
                throw new ArgumentNullException(nameof(learningStep));

            AddNextStepToEndPoint(learningStep, _playerShotStep);
            _cyclicalStep.SetLoopingStep(learningStep);
        }

        private void CreateSteps()
        {
            _playerShotStep = new PlayerShotStep(_playerController);
            _resetComboStep = new ResetComboStep(_comboCounter);
            _bonusActivationStep = new BonusActivationStep(_bulletCollector);
            _prepareActorsStep = new PrepareActorsStep(_actorsController);
            _objectsMoveStep = new ActorsMoveStep(_actorsController);
            _enemyAttackStep = new EnemyAttackStep(_actorsController);
            _removeActorsStep = new RemoveActorsStep(_actorsController);
            _rewardStep = new RewardStep(_finishWindow);
            _closeSceneStep = new CloseSceneStep(_mainMenuLoader);
        }

        private void ConnectSteps()
        {
            AddNextStepToEndPoint(_turret, _resetComboStep);
            AddNextStepToEndPoint(_resetComboStep, _bonusActivationStep);
            AddNextStepToEndPoint(_bonusActivationStep, _prepareActorsStep);
            AddNextStepToEndPoint(_objectsMoveStep, _enemyAttackStep);
            AddNextStepToEndPoint(_enemyAttackStep, _removeActorsStep);
            AddNextStepToEndPoint(_rewardStep, _closeSceneStep);
        }

        private void AddNextStepToEndPoint(IEndPointStep endPointStep, IStep nextStep)
        {
            IEndStep endStep = new NextStep(_stepSystem, nextStep);
            endPointStep.SetEndStep(endStep);
        }

        private void CreateFinalSteps()
        {
            DynamicNextStep dynamicNextStep = new DynamicNextStep(_stepSystem);
            _cyclicalStep = new CyclicalStep(_actorsController, dynamicNextStep, _turret);
            _cyclicalStep.SetStartStep(_prepareActorsStep);
            _cyclicalStep.SetLoopingStep(_playerShotStep);
            _cyclicalStep.SetFinishStep(_rewardStep);

            AddNextStepToEndPoint(_removeActorsStep, _cyclicalStep);
        }

        private void CreateDynamicNextStepPrepareActors()
        {
            _nextStepPrepareActors = new DynamicNextStep(_stepSystem);
            _prepareActorsStep.SetEndStep(_nextStepPrepareActors);
            _nextStepPrepareActors.SetNextStep(_objectsMoveStep);
        }
    }
}
