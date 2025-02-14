using System;
using System.Collections.Generic;

public class BonusGathererBullet : IBonusGatherer
{
    private List<IBonus> _selectedBonuses;

    public void Gather(IBonus bonus)
    {
        if (bonus == null)
            throw new ArgumentNullException(nameof(bonus));

        _selectedBonuses ??= new List<IBonus>();
        _selectedBonuses.Add(bonus);
    }

    public bool TryGetBonuses(out List<IBonus> bonuses)
    {
        bonuses = null;

        if (_selectedBonuses == null || _selectedBonuses.Count == 0)
            return false;

        bonuses = _selectedBonuses;
        _selectedBonuses = null;

        return true;
    }
}