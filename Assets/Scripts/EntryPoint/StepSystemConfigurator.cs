using System;
using UnityEngine;

public class StepSystemConfigurator : MonoBehaviour
{
    [SerializeField] private StepSystem _stepSystem;
    [SerializeField] private BulletCollector _bulletCollector;

    private Player _player;
    private ActorsController _actorsController;

    private IStep _playerShotStep;
    private BonusActivationStep _bonusActivationStep;
    private PrepareActorsStep _prepareActorsStep;
    private ActorsMoveStep _objectsMoveStep;
    private EnemyAttackStep _enemyAttackStep;
    private RemoveActorsStep _removeActorsStep;

    private void OnValidate()
    {
        if (_stepSystem == null)
            throw new NullReferenceException(nameof(_stepSystem));
    }

    public void Configure(Player player, ActorsController actorsController)
    {
        if (player == null) 
            throw new NullReferenceException(nameof(player));

        if (actorsController == null)
            throw new NullReferenceException(nameof(actorsController));

        _player = player;
        _actorsController = actorsController;

        CreateSteps();
        ConnectSteps();

        _stepSystem.EstablishNextStep(_playerShotStep);
    }

    private void CreateSteps()
    {
        _playerShotStep = new PlayerShotStep(_player);
        _bonusActivationStep = new BonusActivationStep(_bulletCollector);
        _prepareActorsStep = new PrepareActorsStep(_actorsController);
        _objectsMoveStep = new ActorsMoveStep(_actorsController.ActorsMover);
        _enemyAttackStep = new EnemyAttackStep(_actorsController.EnemyAttacker);
        _removeActorsStep = new RemoveActorsStep(_actorsController.ActorsRemover);
    }

    private void ConnectSteps()
    {
        AddNextStepToEndPoint(_player, _bonusActivationStep);
        AddNextStepToEndPoint(_bonusActivationStep, _prepareActorsStep);
        AddNextStepToEndPoint(_prepareActorsStep, _objectsMoveStep);
        AddNextStepToEndPoint(_objectsMoveStep, _enemyAttackStep);
        AddNextStepToEndPoint(_enemyAttackStep, _removeActorsStep);
        AddNextStepToEndPoint(_removeActorsStep, _playerShotStep);
    }

    private void AddNextStepToEndPoint(IEndPointStep endPointStep, IStep nextStep)
    {
        IEndStep endStep = new NextStep(_stepSystem, nextStep);
        endPointStep.SetEndStep(endStep);
    }
}
