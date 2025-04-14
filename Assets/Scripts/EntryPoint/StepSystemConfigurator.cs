using System;
using UnityEngine;

public class StepSystemConfigurator : MonoBehaviour
{
    [SerializeField] private StepSystem _stepSystem;
    [SerializeField] private BulletsCollector _bulletCollector;

    private IPlayer _player;
    private ITurret _turret;
    private ActorsController _actorsController;

    private PlayerShotStep _playerShotStep;
    private BonusActivationStep _bonusActivationStep;
    private PrepareActorsStep _prepareActorsStep;
    private ActorsMoveStep _objectsMoveStep;
    private EnemyAttackStep _enemyAttackStep;
    private RemoveActorsStep _removeActorsStep;
    private CloseSceneStep _closeSceneStep;
    private FinalStep _finalStep;

    private void OnValidate()
    {
        if (_stepSystem == null)
            throw new NullReferenceException(nameof(_stepSystem));
    }

    public void Configure(IPlayer player, ITurret turret, ActorsController actorsController)
    {
        _player = player ?? throw new NullReferenceException(nameof(player));
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
        _closeSceneStep = new CloseSceneStep();
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
        _finalStep.SetCloseSceneStep(_closeSceneStep);

        AddNextStepToEndPoint(_removeActorsStep, _finalStep);
    }
}
