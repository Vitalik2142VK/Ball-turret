using System;
using UnityEngine;

public class FullHealthTurretView : MonoBehaviour, IBonusActicatorView
{
    [SerializeField] private ParticleSystem _fullTurretParticle;
    [SerializeField] private Sound _turretRepairSound;

    private void OnValidate()
    {
        if (_fullTurretParticle == null)
            throw new NullReferenceException(nameof(_fullTurretParticle));

        if (_turretRepairSound == null)
            throw new NullReferenceException(nameof(_turretRepairSound));
    }

    public void PlayActivation()
    {
        Debug.Log("FullHealthTurretView.PlayActivation");

        _fullTurretParticle.Play();
        _turretRepairSound.Play();
    }
}