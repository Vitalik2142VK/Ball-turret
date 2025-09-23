using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class BorderView : MonoBehaviour, IBorderView
{
    [field: SerializeField] public HealthBar HealthBar { get; private set; }

    private IBorder _model;
    private ISound _destroySound;

    public string Name => name;
    public IActor Actor => _model;

    private void OnValidate()
    {
        if (HealthBar == null)
            throw new NullReferenceException(nameof(HealthBar));
    }

    private void Awake()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }

    private void OnEnable()
    {
        _model?.Enable();
    }

    private void OnDisable()
    {
        _model.Disable();
    }

    public void Initialize(IBorder model, ISound destroySound)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _destroySound = destroySound ?? throw new ArgumentNullException(nameof(destroySound));
        _model.Enable();
    }

    public void TakeDamage(IDamageAttributes damage) => _model.TakeDamage(damage);

    public void IgnoreArmor(IDamageAttributes damage) => _model.IgnoreArmor(damage);


    public void Destroy()
    {
        _destroySound.Play();
        Destroy(gameObject);
    }

    public void PlayDamage()
    {
        throw new NotImplementedException();
    }
}
