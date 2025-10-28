using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExplosionView : MonoBehaviour, IExplosionView
{
    private ParticleSystem _explosionParticle;

    private void Awake()
    {
        _explosionParticle = GetComponent<ParticleSystem>();
    }

    public void Play() => _explosionParticle.Play();
}