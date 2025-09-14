using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
public class EnemyView : MonoBehaviour, IEnemyView
{
    [SerializeField, SerializeIterface(typeof(IDebuffReceiver))] private GameObject _debuffReceiverGameObject;

    [field: SerializeField] public HealthBar HealthBar { get; }

    private IEnemy _model;
    private ISound _destroySound;

    public string Name => name;

    public IDebuffReceiver DebuffReceiver { get; private set; }

    private void OnValidate()
    {
        if (_debuffReceiverGameObject == null)
            throw new NullReferenceException(nameof(_debuffReceiverGameObject));

        if (HealthBar == null)
            throw new NullReferenceException(nameof(HealthBar));
    }

    private void Awake()
    {
        DebuffReceiver = _debuffReceiverGameObject.GetComponent<IDebuffReceiver>();

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }

    private void OnEnable()
    {
        _model.Enable();
    }

    private void OnDisable()
    {
        _model.Disable();
    }

    public void Initialize(IEnemy model)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
    }

    public void TakeDamage(IDamageAttributes damage) => _model.TakeDamage(damage);

    public void PlayDamage()
    {
        throw new NotImplementedException();
    }

    public void PlayMovement()
    {
        throw new NotImplementedException();
    }

    public void Destroy()
    {
        _destroySound.Play();
        Destroy(gameObject);
    }
}