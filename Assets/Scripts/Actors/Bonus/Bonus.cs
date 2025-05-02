using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Mover))]
public class Bonus : MonoBehaviour, IBonus
{
    [SerializeField] private Image _image;
    [SerializeField] private Scriptable.BonusCard _bonusCard;

    private IMover _mover;
    private IBonusActivator _activator;

    public IMover Mover => _mover;
    public bool IsEnable { get; private set; }

    public string Name => _bonusCard.Name;
    public IBonusCard BonusCard => _bonusCard;

    private void OnValidate()
    {
        if (_image == null)
            throw new NullReferenceException(nameof(_image));

        if (_bonusCard == null)
            throw new NullReferenceException(nameof(_bonusCard));
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _image.sprite = _bonusCard.Icon;
    }

    private void OnEnable()
    {
        IsEnable = true;
    }

    private void OnDisable()
    {
        IsEnable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IBonusGatherer gatheringBonus))
        {
            gatheringBonus.Gather(this);

            Destroy();
        }
    }

    public void Initialize(IBonusActivator bonusActivator)
    {
        _activator = bonusActivator ?? throw new ArgumentNullException(nameof(bonusActivator));
    }

    public void SetStartPosition(Vector3 startPosition)
    {
        _mover.SetStartPosition(startPosition);
    }

    public void Activate()
    {
        _activator.Activate();
    }

    public IBonusActivator GetCloneBonusActivator()
    {
        return _activator.Clone();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
