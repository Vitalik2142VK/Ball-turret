using System.Collections.Generic;

public interface IBonusReservator
{
    public IEnumerable<IReservatedBonus> Bonuses { get; } 
    public bool IsBonusActivated { get; }
    public bool HasBonuses { get; }

    public void ActivateBonus(string nameBonus);

    public bool TryAddBonusByName(string nameBonus);

    public void Update();
}