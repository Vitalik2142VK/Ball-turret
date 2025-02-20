using System;
using UnityEngine;

[RequireComponent(typeof(TurretConfigurator), typeof(StepSystemConfigurator), typeof(ActorsConfigurator))]
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Player _player;

    private TurretConfigurator _turretConfigurator;
    private StepSystemConfigurator _stepSystemConfigurator;
    private ActorsConfigurator _actorsConfigurator;

    private void OnValidate()
    {
        if (_player == null)
            throw new NullReferenceException(nameof(_player));
    }

    private void Awake()
    {
        _turretConfigurator = GetComponent<TurretConfigurator>();
        _stepSystemConfigurator = GetComponent<StepSystemConfigurator>();
        _actorsConfigurator = GetComponent<ActorsConfigurator>();
    }

    private void Start()
    {
        _turretConfigurator.Configure();

        ITurret turret = _turretConfigurator.Turret;

        _player.Initialize(turret);
        _actorsConfigurator.Configure(turret);
        _stepSystemConfigurator.Configure(_player, _actorsConfigurator.ActorsController);
    }
}
