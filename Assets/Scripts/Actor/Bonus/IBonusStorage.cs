﻿using System.Collections.Generic;

public interface IBonusStorage
{
    public bool TryGetBonuses(out IReadOnlyCollection<IBonus> bonuses);
}