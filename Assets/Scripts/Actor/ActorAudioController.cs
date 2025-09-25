using System;
using UnityEngine;

public class ActorAudioController : MonoBehaviour, IActorAudioController
{
    [SerializeField] private Sound _deadSound;
    [SerializeField] private Sound _hitSound;

    private void OnValidate()
    {
        if (_deadSound == null)
            throw new NullReferenceException(nameof(_deadSound));

        if (_hitSound == null)
            throw new NullReferenceException(nameof(_hitSound));
    }

    public void PlayHit() => _hitSound.Play();

    public void PlayDead() => _deadSound.Play();
}