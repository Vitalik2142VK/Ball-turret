using System;
using UnityEngine;
using Scriptable;

public class BorderCreator : MonoBehaviour
{
    [SerializeField] private BorderView _borderPrefab;
    [SerializeField] private ArmorAttributes _armorAttributes;
    [SerializeField] private HealthAttributes _healthAttributes;
    [SerializeField] private Sound _destroySound;

    public string Name => _borderPrefab.Name;

    private void OnValidate()
    {
        if (_borderPrefab == null)
            throw new ArgumentNullException(nameof(_borderPrefab));

        if (_armorAttributes == null)
            throw new ArgumentNullException(nameof(_armorAttributes));

        if (_healthAttributes == null)
            throw new ArgumentNullException(nameof(_healthAttributes));

        if (_destroySound == null)
            throw new NullReferenceException(nameof(_destroySound));
    }

    public IBorder Create(IActorHealthModifier healthModifier)
    {
        if (healthModifier == null) 
            throw new ArgumentNullException(nameof(healthModifier));

        _armorAttributes.CalculateArmor();

        BorderView view = Instantiate(_borderPrefab, Vector3.zero, _borderPrefab.transform.rotation);
        HealthImprover healthImprover = new HealthImprover(_healthAttributes);
        healthImprover.Improve(healthModifier.HealthCoefficient);

        HealthBar healthBar = view.HealthBar;
        Health health = new Health(healthImprover, healthBar);
        Armor armor = new Armor(health, _armorAttributes);
        health.Restore();

        Mover mover = new Mover(view.transform);
        Border model = new Border(view, mover, armor, health);
        view.Initialize(model, _destroySound);

        return model;
    }
}