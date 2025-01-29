using System;
using UnityEngine;

public class Step : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Turret _turret;

    private void OnValidate()
    {
        if (_player == null)
            throw new NullReferenceException(nameof(_player));

        if (_turret == null)
            throw new NullReferenceException(nameof(_turret));
    }

    private void OnEnable()
    {
        _turret.GunFired += OnFreezePlayer;
    }

    private void OnDisable()
    {
        _turret.GunFired -= OnFreezePlayer;
    }

    private void OnFreezePlayer()
    {
        _player.SetWaiting(true);
    }
}
