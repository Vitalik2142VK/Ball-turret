using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Bonus), typeof(Mover))]
public class CollisionBonus : MonoBehaviour, IBonus, IActor
{
    [SerializeField] private Image _image;
    [SerializeField] private Bonus _bonus;

    private IMover _mover;
    private ISound _sound;

    public string Name => _bonus.Name;
    public IBonusCard BonusCard => _bonus.BonusCard;
    public IMover Mover => _mover;

    public bool IsEnable { get; private set; }

    private void OnValidate()
    {
        if (_image == null)
            throw new NullReferenceException(nameof(_image));

        if (_bonus == null)
            _bonus = GetComponent<Bonus>();
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();

        _image.sprite = _bonus.BonusCard.Icon;
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

            _sound.Play();

            Destroy();
        }
    }

    public void Initialize(IBonusActivator bonusActivator, ISound sound) {
        _sound = sound ?? throw new ArgumentNullException(nameof(sound));

        _bonus.Initialize(bonusActivator);
    }

    public void SetStartPosition(Vector3 startPosition) => _mover.SetStartPosition(startPosition);

    public void Activate() => _bonus.Activate();

    public IBonusActivator GetCloneBonusActivator() => _bonus.GetCloneBonusActivator();

    public void Destroy()
    {
        Destroy(gameObject);
    }
}