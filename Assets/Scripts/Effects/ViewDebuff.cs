using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ViewDebuff : MonoBehaviour, IViewDebuff
{
    [SerializeField] private DebuffType _debuffType;

    private ParticleSystem _particleSystem;

    public DebuffType DebuffType => _debuffType;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void SetActive(bool isActive)
    {
        if (isActive)
            _particleSystem.Play();
        else
            _particleSystem.Stop();
    }
}