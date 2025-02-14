using System;
using UnityEngine;

[RequireComponent(typeof(TurretConfigurator), typeof(StepSystemConfigurator))]
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Player _player;

    private TurretConfigurator _turretConfigurator;
    private StepSystemConfigurator _stepSystemConfigurator;

    private void OnValidate()
    {
        if (_player == null)
            throw new NullReferenceException(nameof(_player));
    }

    private void Awake()
    {
        _turretConfigurator = GetComponent<TurretConfigurator>();
        _stepSystemConfigurator = GetComponent<StepSystemConfigurator>();
    }

    private void Start()
    {
        _turretConfigurator.Configure();
        _player.Initialize(_turretConfigurator.Turret);

        _stepSystemConfigurator.Configure(_player);
    }
}
