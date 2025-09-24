using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class BorderView : MonoBehaviour, IBorderView
{
    [field: SerializeField] public HealthBar HealthBar { get; private set; }

    private IBorder _model;
    private ISound _destroySound;
    private Collider _collider;

    public string Name => name;
    public IActor Actor => _model;

    private void OnValidate()
    {
        if (HealthBar == null)
            throw new NullReferenceException(nameof(HealthBar));
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }

    private void OnEnable()
    {
        _collider.enabled = true;
    }

    public void Initialize(IBorder model, ISound destroySound)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _destroySound = destroySound ?? throw new ArgumentNullException(nameof(destroySound));
    }

    public void TakeDamage(IDamageAttributes damage) => _model.TakeDamage(damage);

    public void IgnoreArmor(IDamageAttributes damage) => _model.IgnoreArmor(damage);

    public void PlayDamage()
    {
        throw new NotImplementedException();
    }

    public void PlayDead()
    {
        throw new NotImplementedException();
    }

    public void Destroy()
    {
        _destroySound.Play();
        Destroy(gameObject);
    }
}
