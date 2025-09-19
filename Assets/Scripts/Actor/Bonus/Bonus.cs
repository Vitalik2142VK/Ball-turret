using System;

public class Bonus : IBonus
{
    private IBonusActivator _activator;

    public Bonus(IBonusCard bonusCard, IBonusActivator bonusActivator)
    {
        _activator ??= bonusActivator ?? throw new ArgumentNullException(nameof(bonusActivator));

        BonusCard = bonusCard ?? throw new ArgumentNullException(nameof(bonusCard));
    }

    public IBonusCard BonusCard { get; }

    public void Activate()
    {
        _activator.Activate();
    }

    public IBonusActivator GetCloneBonusActivator()
    {
        return _activator.Clone();
    }
}
