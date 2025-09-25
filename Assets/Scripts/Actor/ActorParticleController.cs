using System;
using UnityEngine;

public class ActorParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hit;
    [SerializeField] private ParticleSystem _dead;

    public float TimeLiveDeadParticles { get; private set; }

    private void OnValidate()
    {
        if (_hit == null)
            throw new NullReferenceException(nameof(_hit));

        if (_dead == null)
            throw new NullReferenceException(nameof(_dead));
    }

    public void PlayHit() => _hit.Play();

    public void PlayDead()
    {
        _dead.Play();
        var main = _dead.main;
        TimeLiveDeadParticles = main.duration + main.startLifetime.constantMax;
    }
}
