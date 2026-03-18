using CannonTurret.Actors;
using CannonTurret.Actors.Bonuses.Activators;
using CannonTurret.Actors.Bonuses.ReserveredBonuses;
using CannonTurret.Actors.Enemies;
using CannonTurret.Effects.Freezing;
using CannonTurret.StepSystem;
using CannonTurret.StepSystem.Steps;
using CannonTurret.Turrets.Bullets;
using CannonTurret.UI;
using CannonTurret.UI.PlayerScene;
using System;
using UnityEngine;

namespace CannonTurret.EntryPoints.PlayLevel.Configurators
{
    public class StepControllerConfigurator : MonoBehaviour
    {
        [SerializeField] private StepController _stepController;
        [SerializeField] private ComboCounter _comboCounter;
        [SerializeField] private BulletsCollector _bulletCollector;
        [SerializeField] private FinishWindow _finishWindow;
        [SerializeField] private FreezingBonusActivatorCreator _freezerCreator;
        [SerializeField] private OpenWindowButton _openReservedBonusesButton;
        [SerializeField] private ReservedBonusesWindow _reservedBonusesWindow;
        [SerializeField] private ActorsFreezerView _freezer;

        private IDataForStepSystem _dataForStepSystem;
        private IDynamicEndStep _nextStepPrepareActors;
        private PlayerStep _playerStep;
        private ResetComboStep _resetComboStep;
        private BonusActivationStep _bonusActivationStep;
        private PrepareActorsStep _prepareActorsStep;
        private ActorsFreezeStep _actorsFreezeStep;
        private ActorsMoveStep _objectsMoveStep;
        private EnemyAttackStep _enemyAttackStep;
        private RemoveActorsStep _removeActorsStep;
        private CyclicalStep _cyclicalStep;
        private RewardStep _rewardStep;
        private PlayVictoryStep _playVictoryStep;

        public IChangeSceneStep ChangeSceneStep { get; private set; }

        private void OnValidate()
        {
            if (_stepController == null)
                throw new NullReferenceException(nameof(_stepController));

            if (_comboCounter == null)
                throw new NullReferenceException(nameof(_comboCounter));

            if (_bulletCollector == null)
                throw new NullReferenceException(nameof(_bulletCollector));

            if (_finishWindow == null)
                throw new NullReferenceException(nameof(_finishWindow));

            if (_freezerCreator == null)
                throw new NullReferenceException(nameof(_freezerCreator));

            if (_openReservedBonusesButton == null)
                throw new NullReferenceException(nameof(_openReservedBonusesButton));

            if (_reservedBonusesWindow == null)
                throw new NullReferenceException(nameof(_reservedBonusesWindow));

            if (_freezer == null)
                throw new NullReferenceException(nameof(_freezer));
        }

        public void Configure(IDataForStepSystem dataForStepSystem)
        {
            _dataForStepSystem = dataForStepSystem ?? throw new NullReferenceException(nameof(dataForStepSystem));

            var controllersAccess = _dataForStepSystem.ControllersAccess;
            var actorsController = controllersAccess.ActorsController;
            var enemiesController = controllersAccess.EnemiesController;

            CreateSteps(actorsController, enemiesController);
            CreatePrepareActorsStep(actorsController, enemiesController);
            CreateCyclicalStep(actorsController, enemiesController);
            ConnectSteps();
            CreateActorsFreezeStep();

            _stepController.EstablishNextStep(_cyclicalStep);
            _freezerCreator.Initialize(_nextStepPrepareActors, _actorsFreezeStep);
        }

        public void ConfigureBonusActivationStep(IBonusReservator bonusReservator)
        {
            if (bonusReservator == null)
                throw new NullReferenceException(nameof(bonusReservator));

            _bonusActivationStep.Initialize(bonusReservator);
        }

        public void AddLearningStep(LearningStep learningStep)
        {
            if (learningStep == null)
                throw new ArgumentNullException(nameof(learningStep));

            AddNextStepToEndPoint(learningStep, _playerStep);
            _cyclicalStep.SetLoopingStep(learningStep);
        }

        public void ChangeFinishWindow(IWindow window)
        {
            if (window == null)
                throw new ArgumentNullException(nameof(window));

            _rewardStep.SetFinishWindow(window);
        }

        private void CreateSteps(IActorsController actorsController, IEnemiesController enemiesController)
        {
            _playerStep = new PlayerStep(_dataForStepSystem.PlayerController, enemiesController, _reservedBonusesWindow);
            _resetComboStep = new ResetComboStep(_comboCounter);
            _bonusActivationStep = new BonusActivationStep(_bulletCollector, _openReservedBonusesButton);
            _objectsMoveStep = new ActorsMoveStep(actorsController);
            _enemyAttackStep = new EnemyAttackStep(enemiesController);
            _removeActorsStep = new RemoveActorsStep(actorsController);
            _rewardStep = new RewardStep(_finishWindow, _dataForStepSystem.AdsViewer, _dataForStepSystem.RewardIssuer);
            _playVictoryStep = new PlayVictoryStep(_dataForStepSystem.VictoryController);

            ChangeSceneStep = new ChangeSceneStep();
        }

        private void CreatePrepareActorsStep(IActorsController actorsController, IEnemiesController enemiesController)
        {
            _nextStepPrepareActors = new DynamicNextStep(_stepController);
            _prepareActorsStep = new PrepareActorsStep(actorsController, enemiesController, _nextStepPrepareActors, _objectsMoveStep);
        }

        private void CreateCyclicalStep(IActorsRemover actorsRemover, IEnemiesController enemiesController)
        {
            DynamicNextStep dynamicNextStep = new DynamicNextStep(_stepController);
            _cyclicalStep = new CyclicalStep(dynamicNextStep, actorsRemover, enemiesController, _dataForStepSystem.LevelStatus);
            _cyclicalStep.SetStartStep(_prepareActorsStep);
            _cyclicalStep.SetLoopingStep(_playerStep);
            _cyclicalStep.SetFinishStep(_rewardStep);

            AddNextStepToEndPoint(_removeActorsStep, _cyclicalStep);
        }

        private void CreateActorsFreezeStep()
        {
            _actorsFreezeStep = new ActorsFreezeStep(_nextStepPrepareActors, _objectsMoveStep, _freezer);

            AddNextStepToEndPoint(_actorsFreezeStep, _removeActorsStep);
        }

        private void ConnectSteps()
        {
            AddNextStepToEndPoint(_playerStep, _resetComboStep);
            AddNextStepToEndPoint(_dataForStepSystem.Turret, _resetComboStep);
            AddNextStepToEndPoint(_resetComboStep, _bonusActivationStep);
            AddNextStepToEndPoint(_bonusActivationStep, _prepareActorsStep);
            AddNextStepToEndPoint(_objectsMoveStep, _enemyAttackStep);
            AddNextStepToEndPoint(_enemyAttackStep, _removeActorsStep);
            AddNextStepToEndPoint(_rewardStep, _playVictoryStep);
            AddNextStepToEndPoint(_playVictoryStep, ChangeSceneStep);
        }

        private void AddNextStepToEndPoint(IEndPointStep endPointStep, IStep nextStep)
        {
            IEndStep endStep = new NextStep(_stepController, nextStep);
            endPointStep.SetEndStep(endStep);
        }
    }
}
