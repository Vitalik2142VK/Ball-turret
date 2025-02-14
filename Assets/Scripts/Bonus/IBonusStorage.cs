using System.Collections.Generic;

public interface IBonusStorage
{
    public bool TryGetBonuses(out List<IBonus> bonuses);
}