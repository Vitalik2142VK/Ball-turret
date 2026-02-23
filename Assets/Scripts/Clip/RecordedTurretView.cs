using System;
using UnityEngine;

public class RecordedTurretView : MonoBehaviour
{
    private const string Shot = nameof(Shot);

    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _shotParticles;
    [SerializeField] private Sound _shotSound;
    [SerializeField] private ShooterView _shooterView;

    private int _hashShot;

    private void OnValidate()
    {
        if (_animator == null)
            throw new NullReferenceException(nameof(_animator));

        if (_shotParticles == null)
            throw new NullReferenceException(nameof(_shotParticles));

        if (_shotSound == null)
            throw new NullReferenceException(nameof(_shotSound));

        if (_shooterView == null)
            throw new NullReferenceException(nameof(_shooterView));
    }

    private void Awake()
    {
        _hashShot = Animator.StringToHash(Shot);
    }

    public void PlayShoot()
    {
        _animator.SetTrigger(_hashShot);
        _shotParticles.Play();
        _shotSound.Play();
        _shooterView.PlayShot();
    }
}