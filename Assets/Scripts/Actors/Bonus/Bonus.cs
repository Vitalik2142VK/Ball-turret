using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Bonus : MonoBehaviour, IBonus
{
    [SerializeField, SerializeIterface(typeof(IBonusActivator))] private GameObject _bonusActivator;

    private IMover _mover;
    private IBonusActivator _activator;

    public IMover Mover => _mover;
    public bool IsEnable { get; private set; }

    public string Name => name;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _activator = _bonusActivator.GetComponent<IBonusActivator>();
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

    public void SetBonusActivator(IBonusActivator bonusActivator)
    {
        _activator.Initialize(bonusActivator);
    }

    public void SetStartPosition(Vector3 startPosition)
    {
        _mover.SetStartPosition(startPosition);
    }

    public void Activate()
    {
        _activator.Activate();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
