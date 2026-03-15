using System.Collections.Generic;

public interface IBonusRandomizer
{
    public IEnumerable<IBonus> GetBonuses(int countBonuses);
}
