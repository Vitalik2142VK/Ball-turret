using System;
using UnityEngine;

public class Bonus : MonoBehaviour, IBonus
{
    [SerializeField] private Scriptable.BonusCard _bonusCard;

    private IBonusActivator _activator;

    public string Name => BonusCard.Name;
    public IBonusCard BonusCard => _bonusCard;

    private void OnValidate()
    {
        if (_bonusCard == null)
            throw new NullReferenceException(nameof(_bonusCard));
    }

    public void Initialize(IBonusActivator bonusActivator)
    {
        _activator = bonusActivator ?? throw new ArgumentNullException(nameof(bonusActivator));
    }

    public void Activate()
    {
        _activator.Activate();
    }

    public IBonusActivator GetCloneBonusActivator()
    {
        return _activator.Clone();
    }
}
