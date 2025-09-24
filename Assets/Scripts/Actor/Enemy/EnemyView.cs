using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody), typeof(EnemyAnimator))]
public class EnemyView : MonoBehaviour, IEnemyView
{
    [SerializeField, SerializeIterface(typeof(IDebuffReceiver))] private GameObject _debuffReceiverGameObject;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private EnemyParticleController _particleController;
    [SerializeField] private Image _shadow;

    [field: SerializeField] public HealthBar HealthBar { get; private set; }

    private IEnemy _model;
    private IEnemyAudioController _audioController;
    private ISound _destroySound;
    private Collider _collider;
    private EnemyAnimator _enemyAnimator;

    public string Name => name;
    public IActor Actor => _model;

    public IDebuffReceiver DebuffReceiver { get; private set; }

    private void OnValidate()
    {
        if (_debuffReceiverGameObject == null)
            throw new NullReferenceException(nameof(_debuffReceiverGameObject));

        if (_meshRenderer == null)
            throw new NullReferenceException(nameof(_meshRenderer));

        if (_particleController == null)
            throw new NullReferenceException(nameof(_particleController));

        if (HealthBar == null)
            throw new NullReferenceException(nameof(HealthBar));
    }

    private void Awake()
    {
        DebuffReceiver = _debuffReceiverGameObject.GetComponent<IDebuffReceiver>();
        _collider = _meshRenderer.GetComponent<CapsuleCollider>();
        _enemyAnimator = GetComponent<EnemyAnimator>();

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }

    private void OnEnable()
    {
        SetEnable(true);
    }

    public void Initialize(IEnemy model, ISound destroySound)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _destroySound = destroySound ?? throw new ArgumentNullException(nameof(destroySound)); ;
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
        StartCoroutine(StartDeadProcess());
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void SetEnable(bool isEnable)
    {
        _collider.enabled = isEnable;
        _meshRenderer.enabled = isEnable;
        _shadow.enabled = isEnable;
    }

    private IEnumerator StartDeadProcess()
    {
        _collider.enabled = false;
        _enemyAnimator.PlayDead();

        yield return new WaitForSeconds(_enemyAnimator.TimeCompletionDeath);

        SetEnable(false);

        _audioController.PlayDead();
        _particleController.PlayDead();

        yield return new WaitForSeconds(_particleController.TimeLiveDeadParticles);

        _model.Destroy();
    }
}
