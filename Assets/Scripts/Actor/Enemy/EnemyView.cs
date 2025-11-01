using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody), typeof(EnemyAnimator))]
public class EnemyView : MonoBehaviour, IEnemyView
{
    [SerializeField, SerializeIterface(typeof(IDebuffHandler))] private GameObject _debuffReceiverGameObject;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private ActorParticleController _particleController;
    [SerializeField] private Image _shadow;

    [field: SerializeField] public HealthBar HealthBar { get; private set; }

    private IEnemyPresenter _presenter;
    private IEnemyAnimator _enemyAnimator;
    private IActorAudioController _audioController;
    private Collider _collider;

    public string Name => name;

    public IDebuffHandler DebuffReceiver { get; private set; }
    public bool IsActive { get; private set; }

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
        DebuffReceiver = _debuffReceiverGameObject.GetComponent<IDebuffHandler>();
        _collider = GetComponent<CapsuleCollider>();
        _enemyAnimator = GetComponent<EnemyAnimator>();

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }

    private void OnEnable()
    {
        SetEnable(true);
    }

    public void Initialize(IEnemyPresenter presenter, IActorAudioController audioController)
    {
        _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        _audioController = audioController ?? throw new ArgumentNullException(nameof(audioController)); ;
    }

    public void PrepareDeleted(IRemovedActorsCollector removedCollector) => _presenter.PrepareDeleted(removedCollector);

    public void PrepareAttacked(IAttackingEnemiesCollector attackingCollector) => _presenter.PrepareAttacked(attackingCollector);

    public void AddDebuff(IDebuff debaff) => _presenter.AddDebuff(debaff);

    public void TakeDamage(IDamageAttributes damage) => _presenter.TakeDamage(damage);

    public void PlayDamage()
    {
        _enemyAnimator.PlayHit();
        _particleController.PlayHit();
        _audioController.PlayHit();
    }

    public void PlayMovement(bool isMovinng)
    {
        _enemyAnimator.PlayMovement(isMovinng);
    }

    public void PlayDead()
    {
        if (IsActive)
            StartCoroutine(StartDeadProcess());
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void SetEnable(bool isEnable)
    {
        IsActive = isEnable;
        _collider.enabled = isEnable;
        _meshRenderer.enabled = isEnable;
        _shadow.gameObject.SetActive(isEnable);
    }

    private IEnumerator StartDeadProcess()
    {
        IsActive = false;
        HealthBar.SetActive(IsActive);
        _collider.enabled = IsActive;
        _enemyAnimator.PlayDead();

        yield return new WaitForSeconds(_enemyAnimator.TimeCompletionDeath);

        SetEnable(IsActive);

        _audioController.PlayDead();
        _particleController.PlayDead();

        yield return new WaitForSeconds(_particleController.TimeLiveDeadParticle);

        _presenter.Destroy();
    }
}
