using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class BonusView : MonoBehaviour, IBonusView
{
    [SerializeField] private BonusTrigger _trigger;
    [SerializeField] private Image _image;
    [SerializeField] private ParticleSystem _destroyParticle;
    [SerializeField] private MeshRenderer _meshRenderer;

    private IBonusPresenter _presenter;
    private ISound _takedSound;

    public string Name => name;

    private void OnValidate()
    {
        if (_trigger == null)
            throw new NullReferenceException(nameof(_trigger));

        if (_image == null)
            throw new NullReferenceException(nameof(_image));

        if (_destroyParticle == null)
            throw new NullReferenceException(nameof(_destroyParticle));

        if (_meshRenderer == null)
            throw new NullReferenceException(nameof(_meshRenderer));
    }

    private void Awake()
    {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = false;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }

    private void OnEnable()
    {
        SetEnable(true);

        _trigger.SetAvtive(true);
        _trigger.Activated += OnHandleTrigger;
    }

    private void OnDisable()
    {
        _trigger.Activated -= OnHandleTrigger;
        _trigger.SetAvtive(false);
    }

    public void Initialize(IBonusPresenter presenter, IBonusCard bonusCard, ISound takedSound)
    {
        _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        _takedSound = takedSound ?? throw new ArgumentNullException(nameof(takedSound));

        if (bonusCard == null)
            throw new ArgumentNullException(nameof(bonusCard));

        _image.sprite = bonusCard.Icon;
    }

    public void PrepareDeleted(IRemovedActorsCollector removedCollector) => _presenter.PrepareDeleted(removedCollector);

    public void PlayTaking()
    {
        _takedSound.Play();
        _trigger.SetAvtive(false);
    }

    public void Destroy()
    {
        _destroyParticle.Play();
        var particleMain = _destroyParticle.main;
        float timeLiveParticle = particleMain.duration + particleMain.startLifetime.constantMax;

        SetEnable(false);
        StartCoroutine(WaitDestroy(timeLiveParticle));
    }

    private void SetEnable(bool isEnable)
    {
        _image.enabled = isEnable;
        _meshRenderer.enabled = isEnable;
    }

    private void OnHandleTrigger(Collider collider)
    {
        if (collider.TryGetComponent(out IBonusGatherer bonusGathering))
            _presenter.HandleBonusGatherer(bonusGathering);
    }

    private IEnumerator WaitDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject);
    }
}
