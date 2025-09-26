using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class BonusView : MonoBehaviour, IBonusView
{
    [SerializeField] private Image _image;

    private IBonusPresenter _presenter;
    private ISound _takedSound;

    public string Name => name;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IBonusGatherer bonusGathering))
            _presenter.HandleBonusGatherer(bonusGathering);
    }

    public void Initialize(IBonusPresenter presenter, IBonusCard bonusCard, ISound takedSound)
    {
        _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        _takedSound = takedSound ?? throw new ArgumentNullException(nameof(takedSound));

        if (bonusCard == null)
            throw new ArgumentNullException(nameof(bonusCard));

        _image.sprite = bonusCard.Icon;
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
