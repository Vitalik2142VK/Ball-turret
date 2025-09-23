using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody), typeof(EnemyAnimator))]
public class EnemyView : MonoBehaviour, IEnemyView
{
    [SerializeField, SerializeIterface(typeof(IDebuffReceiver))] private GameObject _debuffReceiverGameObject;

    [field: SerializeField] public HealthBar HealthBar { get; private set; }

    private IEnemy _model;
    private ISound _destroySound;
    private EnemyAnimator _enemyAnimator;

    public string Name => name;
    public IActor Actor => _model;

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
        _enemyAnimator = GetComponent<EnemyAnimator>();

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

    public void Initialize(IEnemy model, ISound destroySound)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _destroySound = destroySound ?? throw new ArgumentNullException(nameof(destroySound)); ;
        _model.Enable();
    }

    public void TakeDamage(IDamageAttributes damage) => _model.TakeDamage(damage);

    public void PlayDamage()
    {
        _enemyAnimator.PlayHit();
    }

    public void PlayMovement(bool isMovinng)
    {
        _enemyAnimator.PlayMovement(isMovinng);
    }

    public void PlayDead()
    {
        _enemyAnimator.PlayDead();
    }

    public void Destroy()
    {
        _destroySound.Play();
        Destroy(gameObject);
    }
}

public interface IEnemyParticleController
{
    public void PlayHit();

    public void PlayDead();
}

public interface IEnemyAudioController
{
    public void PlayHit();

    public void PlayDead();
}