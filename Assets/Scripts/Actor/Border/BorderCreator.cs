using System;
using UnityEngine;
using Scriptable;

public class BorderCreator : MonoBehaviour
{
    [SerializeField] private BorderView _borderPrefab;
    [SerializeField] private ArmorAttributes _armorAttributes;
    [SerializeField] private HealthAttributes _healthAttributes;
    [SerializeField] private ActorAudioController _actorAudioController;

    public string Name => _borderPrefab.Name;

    private void OnValidate()
    {
        if (_borderPrefab == null)
            throw new ArgumentNullException(nameof(_borderPrefab));

        if (_armorAttributes == null)
            throw new ArgumentNullException(nameof(_armorAttributes));

        if (_healthAttributes == null)
            throw new ArgumentNullException(nameof(_healthAttributes));

        if (_actorAudioController == null)
            throw new ArgumentNullException(nameof(_actorAudioController));
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
        BorderPresenter presenter = new BorderPresenter();
        Border model = new Border(presenter, mover, armor, health);
        view.Initialize(presenter, _actorAudioController);
        presenter.Initialize(model, view);

        return model;
    }
}