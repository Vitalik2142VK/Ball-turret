using System;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TurretConfigurator _turretConfigurator;
    [SerializeField] private StepSystemConfigurator _stepSystemConfigurator;
    [SerializeField] private ActorsConfigurator _actorsConfigurator;
    [SerializeField] private BonusPrefabConfigurator _bonusPrefabConfigurator;

    private void OnValidate()
    {
        if (_player == null)
            throw new NullReferenceException(nameof(_player));

        if (_turretConfigurator == null)
            throw new NullReferenceException(nameof(_turretConfigurator));

        if (_stepSystemConfigurator == null)
            throw new NullReferenceException(nameof(_stepSystemConfigurator));

        if (_actorsConfigurator == null)
            throw new NullReferenceException(nameof(_actorsConfigurator));

        if (_bonusPrefabConfigurator == null)
            throw new NullReferenceException(nameof(_bonusPrefabConfigurator));
    }

    private void Start()
    {
        _turretConfigurator.Configure();

        ITurret turret = _turretConfigurator.Turret;

        _player.Initialize(turret);
        _actorsConfigurator.Configure(turret);
        _stepSystemConfigurator.Configure(_player, _actorsConfigurator.ActorsController);
        _bonusPrefabConfigurator.Configure();
    }
}
