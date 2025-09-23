using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleInitializer : MonoBehaviour, IParticle
{
    private Transform _transform;
    private ParticleSystem _mainParticle;
    private ParticleSystem[] _childParticles;

    private void Awake()
    {
        _transform = transform;
        _mainParticle = GetComponent<ParticleSystem>();
        _childParticles = GetComponentsInChildren<ParticleSystem>();
    }

    public void Initialize(Color color)
    {
        var main = _mainParticle.main;
        main.startColor = color;

        if (_childParticles == null || _childParticles.Length == 0)
            return;

        foreach (var particle in _childParticles)
        {
            main = particle.main;
            main.startColor = color;
        }
    }

    public void Play(Vector3 position)
    {
        _transform.position = position;
        _mainParticle.Play();
    }
}
