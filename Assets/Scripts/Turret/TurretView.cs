using System;
using UnityEngine;

public class TurretView : MonoBehaviour, ITurretView
{
    private const string Shot = nameof(Shot);
    private const string GetHit = nameof(GetHit);

    [Header("Sound")]
    [SerializeField] private Sound _shotSound;
    [SerializeField] private Sound _takeDamageSound;
    [SerializeField] private Sound _destroySound;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _explosionDestroyParticles;
    [SerializeField] private ParticleSystem _shotParticles;

    [Header("Animation")]
    [SerializeField] private Animator _animator;

    private int _hashShot;
    private int _hashGetHit;

    private void OnValidate()
    {
        if (_shotSound == null)
            throw new NullReferenceException(nameof(_shotSound));

        if (_takeDamageSound == null)
            throw new NullReferenceException(nameof(_takeDamageSound));

        if (_destroySound == null)
            throw new NullReferenceException(nameof(_destroySound));

        if (_explosionDestroyParticles == null)
            throw new NullReferenceException(nameof(_explosionDestroyParticles));

        if (_shotParticles == null)
            throw new NullReferenceException(nameof(_shotParticles));

        if (_animator == null)
            throw new NullReferenceException(nameof(_animator));
    }

    private void Awake()
    {
        _hashShot = Animator.StringToHash(Shot);
        _hashGetHit = Animator.StringToHash(GetHit);
    }

    public void PlayDestroy()
    {
        _destroySound.Play();
        _explosionDestroyParticles.Play();
    }

    public void PlayTakeDamage()
    {
        _animator.SetTrigger(_hashGetHit);
        _takeDamageSound.Play();
    }

    public void PlayShoot()
    {
        _animator.SetTrigger(_hashShot);
        _shotParticles.Play();
        _shotSound.Play();
    }
}