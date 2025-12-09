using System;
using UnityEngine;

public class RecordedTurretView : MonoBehaviour
{
    private const string Shot = nameof(Shot);

    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _shotParticles;
    [SerializeField] private Sound _shotSound;

    private int _hashShot;

    private void Awake()
    {
        _hashShot = Animator.StringToHash(Shot);
    }

    public void PlayShoot()
    {
        _animator.SetTrigger(_hashShot);
        _shotParticles.Play();
        _shotSound.Play();
    }
}

