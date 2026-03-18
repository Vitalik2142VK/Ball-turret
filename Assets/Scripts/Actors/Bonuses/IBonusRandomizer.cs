using System.Collections.Generic;

namespace CannonTurret.Actors.Bonuses
{
    public interface IBonusRandomizer
    {
        public IEnumerable<IBonus> GetBonuses(int countBonuses);
    }
}