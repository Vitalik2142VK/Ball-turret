using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class BonusView : MonoBehaviour, IBonusView
{
    [SerializeField] private Image _image;

    private IViewableBonus _model;
    private ISound _takedSound;

    public string Name => name;
    public IActor Actor => _model;

    private void OnValidate()
    {
        if (_image == null)
            throw new NullReferenceException(nameof(_image));
    }

    private void Awake()
    {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnEnable()
    {
        _model?.Enable();
    }

    private void OnDisable()
    {
        _model.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IBonusGatherer gatheringBonus))
            _model.HandleBonusGatherer(gatheringBonus);
    }

    public void Initialize(IViewableBonus model, ISound takedSound)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _takedSound = takedSound ?? throw new ArgumentNullException(nameof(takedSound));
        _model.Enable();
        _image.sprite = _model.BonusCard.Icon;
    }

    public void PlayTaking()
    {
        _takedSound.Play();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}