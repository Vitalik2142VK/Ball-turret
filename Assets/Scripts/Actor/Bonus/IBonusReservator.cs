using System.Collections.Generic;

public interface IBonusReservator
{
    public IEnumerable<IBonusCard> GetBonusCards();

    public void ActivateBonus(string nameBonus);
}