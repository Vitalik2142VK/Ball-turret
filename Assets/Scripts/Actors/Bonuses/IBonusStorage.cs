using System.Collections.Generic;

namespace CannonTurret.Actors.Bonuses
{
    public interface IBonusStorage
    {
        public bool TryGetBonuses(out IReadOnlyCollection<IBonus> bonuses);
    }
}