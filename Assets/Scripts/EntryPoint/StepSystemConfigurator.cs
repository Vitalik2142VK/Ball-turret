using System;
using UnityEngine;

public class StepSystemConfigurator : MonoBehaviour
{
    [SerializeField] private StepSystem _stepSystem;
    [SerializeField] private BulletCollector _bulletCollector;

    private Player _player;
    private PlayerShotStep _playerShotStep;
    private BonusActivationStep _bonusActivationStep;

    private void OnValidate()
    {
        if (_stepSystem == null)
            throw new NullReferenceException(nameof(_stepSystem));
    }

    public void Configure(Player player)
    {
        if (player == null) 
            throw new NullReferenceException(nameof(player));

        _player = player;
        _playerShotStep = new PlayerShotStep(_player);
        _bonusActivationStep = BonusActivationStep();

        AddEntryPointPlayer();

        _stepSystem.EstablishNextStep(_playerShotStep);
    }

    private BonusActivationStep BonusActivationStep()
    {
        IEndStep nextStep = new NextStep(_stepSystem ,_playerShotStep);
        return new BonusActivationStep(nextStep, _bulletCollector);
    }

    private void AddEntryPointPlayer()
    {
        IEndStep nextStep = new NextStep(_stepSystem, _bonusActivationStep);
        _player.SetEndStep(nextStep);
    }
}
