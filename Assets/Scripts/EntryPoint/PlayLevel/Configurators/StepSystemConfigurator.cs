using System;
using UnityEngine;

namespace PlayLevel
{
    public class StepSystemConfigurator : MonoBehaviour
    {
        [SerializeField] private StepSystem _stepSystem;
        [SerializeField] private BulletsCollector _bulletCollector;
        [SerializeField] private MainMenuLoader _mainMenuLoader;
        [SerializeField] private FinishWindow _finishWindow;

        private ITurret _turret;
        private IWinState _winStat;
        private Player _player;
        private ActorsController _actorsController;

        private PlayerShotStep _playerShotStep;
        private BonusActivationStep _bonusActivationStep;
        private PrepareActorsStep _prepareActorsStep;
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

            if (_bulletCollector == null)
                throw new NullReferenceException(nameof(_bulletCollector));

            if (_mainMenuLoader == null)
                throw new NullReferenceException(nameof(_mainMenuLoader));

            if (_finishWindow == null)
                throw new NullReferenceException(nameof(_finishWindow));
        }

        public void Configure(ITurret turret, IWinState winStat, Player player, ActorsController actorsController)
        {
            _player = player != null ? player : throw new NullReferenceException(nameof(player));
            _turret = turret ?? throw new NullReferenceException(nameof(turret));
            _winStat = winStat ?? throw new NullReferenceException(nameof(winStat));
            _actorsController = actorsController ?? throw new NullReferenceException(nameof(actorsController));

            CreateSteps();
            CreateFinalSteps();
            ConnectSteps();

            _stepSystem.EstablishNextStep(_cyclicalStep);
        }

        private void CreateSteps()
        {
            _playerShotStep = new PlayerShotStep(_player);
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
            AddNextStepToEndPoint(_turret, _bonusActivationStep);
            AddNextStepToEndPoint(_bonusActivationStep, _prepareActorsStep);
            AddNextStepToEndPoint(_prepareActorsStep, _objectsMoveStep);
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
            _cyclicalStep = new CyclicalStep(_actorsController, dynamicNextStep, _winStat);
            _cyclicalStep.SetStartStep(_prepareActorsStep);
            _cyclicalStep.SetPlayerStep(_playerShotStep);
            _cyclicalStep.SetFinishStep(_rewardStep);

            AddNextStepToEndPoint(_removeActorsStep, _cyclicalStep);
        }
    }
}
