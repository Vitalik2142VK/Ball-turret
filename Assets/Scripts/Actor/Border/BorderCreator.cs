using System;
using UnityEngine;
using Scriptable;

public class BorderCreator : MonoBehaviour
{
    [SerializeField] private BorderView _borderPrefab;
    [SerializeField] private ArmorAttributes _armorAttributes;
    [SerializeField] private HealthAttributes _healthAttributes;
    [SerializeField] private HealthBar _healthBar;
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

        if (_healthBar == null)
            throw new NullReferenceException(nameof(_healthBar));

        if (_destroySound == null)
            throw new NullReferenceException(nameof(_destroySound));
    }

    public IBorder Create(IActorHealthModifier healthModifier)
    {
        if (healthModifier == null) 
            throw new ArgumentNullException(nameof(healthModifier));

        _armorAttributes.CalculateArmor();

        HealthImprover healthImprover = new HealthImprover(_healthAttributes);
        healthImprover.Improve(healthModifier.HealthCoefficient);

        Health health = new Health(healthImprover, _healthBar);
        Armor armor = new Armor(health, _armorAttributes);
        health.Restore();

        BorderView view = Instantiate(_borderPrefab, Vector3.zero, _borderPrefab.transform.rotation);
        Border model = new Border(view, armor, health);
        BorderPresenter presenter = new BorderPresenter(model);
        view.Initialize(presenter, _destroySound);

        return model;
    }
}