using System.Collections.Generic;

public interface IBonusReservator
{
    public bool IsBonusActivated { get; }
    public IEnumerable<IReservatedBonus> Bonuses { get; } 

    public void ActivateBonus(string nameBonus);

    public bool TryAddBonusByName(string nameBonus);

    public void Update();
}