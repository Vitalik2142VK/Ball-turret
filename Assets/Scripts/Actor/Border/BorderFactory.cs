using System;
using UnityEngine;

public class BorderFactory : MonoBehaviour, IActorFactory
{
    [SerializeField] private Border _borderPrefab;
    [SerializeField] private Sound _destroySound;

    private IActorHealthModifier _healthModifier;

    private void OnValidate()
    {
        if (_borderPrefab == null)
            throw new ArgumentNullException(nameof(_borderPrefab));

        if (_destroySound == null)
            throw new NullReferenceException(nameof(_destroySound));
    }

    public void Initialize(IActorHealthModifier healthModifier)
    {
        _healthModifier = healthModifier ?? throw new ArgumentNullException(nameof(healthModifier));
    }

    public bool IsCanCreate(string nameTypeActor)
    {
        if (nameTypeActor == null || nameTypeActor.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

        return _borderPrefab.Name == nameTypeActor;
    }

    public IActor Create(string nameTypeActor)
    {
        var border = Instantiate(_borderPrefab, Vector3.zero, _borderPrefab.transform.rotation);
        border.Initialize(_destroySound, _healthModifier);

        return border;
    }
}