using System;
using UnityEngine;
using Scriptable;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(Mover))]
public class Border : MonoBehaviour, IBorder
{
    [SerializeField] private ArmorAttributes _armorAttributes;
    [SerializeField] private HealthAttributes _healthAttributes;
    [SerializeField] private HealthBar _healthBar;

    private IArmor _armor;
    private IMover _mover;
    private IHealth _health;
    private ISound _sound;

    public IMover Mover => _mover;
    public string Name => name;

    public bool IsEnable { get; private set; }

    private void OnValidate()
    {
        if (_armorAttributes == null)
            throw new ArgumentNullException(nameof(_armorAttributes));

        if (_healthBar == null)
            throw new NullReferenceException(nameof(_healthBar));
    }

    private void Awake()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;

        _mover = GetComponent<Mover>();
    }

    private void OnEnable()
    {
        IsEnable = true;

        _health?.Restore();
    }

    private void OnDisable()
    {
        IsEnable = false;
    }

    public void Initialize(ISound sound, IActorHealthModifier modifier)
    {
        if (modifier == null)
            throw new ArgumentNullException(nameof(modifier));

        _sound = sound ?? throw new ArgumentNullException(nameof(sound));
        _armorAttributes.CalculateArmor();

        HealthImprover healthImprover = new HealthImprover(_healthAttributes);
        healthImprover.Improve(modifier.HealthCoefficient);

        _health = new Health(healthImprover, _healthBar);
        _armor = new Armor(_health, _armorAttributes);
        _health.Restore();
    }

    public void SetStartPosition(Vector3 startPosition) => _mover.SetStartPosition(startPosition);

    public void TakeDamage(IDamageAttributes damage)
    {
        if (damage == null)
            throw new ArgumentNullException(nameof(damage));

        _armor.ReduceDamage(damage);

        if (_health.IsAlive == false)
            Destroy();
    }

    public void IgnoreArmor(IDamageAttributes attributes)
    {
        if (attributes == null)
            throw new ArgumentNullException(nameof(attributes));

        _health.TakeDamage(attributes);

        if (_health.IsAlive == false)
            Destroy();
    }

    public void Destroy()
    {
        _sound.Play();
        Destroy(gameObject);
    }
}
