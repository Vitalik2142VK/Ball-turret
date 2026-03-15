using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(DamagedObjectAnimator))]
public class BorderView : MonoBehaviour, IBorderView
{
    [SerializeField] private ActorParticleController _particleController;
    [SerializeField] private Image _shadow;

    [field: SerializeField] public HealthBar HealthBar { get; private set; }

    private IBorderPresenter _presenter;
    private IDamagedObjectAnimator _borderAnimator;
    private IActorAudioController _audioController;
    private Collider _collider;

    public string Name => name;

    public bool IsActive { get; private set; }

    private void OnValidate()
    {
        if (_particleController == null)
            throw new NullReferenceException(nameof(_particleController));

        if (_shadow == null)
            throw new NullReferenceException(nameof(_shadow));

        if (HealthBar == null)
            throw new NullReferenceException(nameof(HealthBar));
    }

    private void Awake()
    {
        _borderAnimator = GetComponent<IDamagedObjectAnimator>();
        _collider = GetComponent<Collider>();

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }

    private void OnEnable()
    {
        IsActive = true;
        _collider.enabled = true;
    }

    public void Initialize(IBorderPresenter presenter, IActorAudioController audioController)
    {
        _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        _audioController = audioController ?? throw new ArgumentNullException(nameof(audioController));
    }

    public void PrepareDeleted(IRemovedActorsCollector removedCollector) => _presenter.PrepareDeleted(removedCollector);

    public void TakeDamage(IDamageAttributes damage) => _presenter.TakeDamage(damage);

    public void IgnoreArmor(IDamageAttributes damage) => _presenter.IgnoreArmor(damage);

    public void PlayDamage()
    {
        _borderAnimator.PlayHit();
        _particleController.PlayHit();
        _audioController.PlayHit();
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

    private IEnumerator StartDeadProcess()
    {
        IsActive = false;
        HealthBar.SetActive(IsActive);
        _collider.enabled = IsActive;
        _borderAnimator.PlayDead();
        _particleController.PlayDead();
        _audioController.PlayDead();

        float timeWait;

        if (_borderAnimator.TimeCompletionDeath >= _particleController.TimeLiveDeadParticle)
            timeWait = _borderAnimator.TimeCompletionDeath;
        else
            timeWait = _particleController.TimeLiveDeadParticle;

        yield return new WaitForSeconds(timeWait);

        _presenter.FinishDeath();
    }
}
