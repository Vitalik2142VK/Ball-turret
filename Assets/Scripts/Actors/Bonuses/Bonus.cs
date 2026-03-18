using CannonTurret.Actors.Bonuses.Activators;
using System;

namespace CannonTurret.Actors.Bonuses
{
    public class Bonus : IBonus
    {
        private readonly IBonusActivator Activator;

        public Bonus(IBonusCard bonusCard, IBonusActivator bonusActivator)
        {
            Activator ??= bonusActivator ?? throw new ArgumentNullException(nameof(bonusActivator));

            BonusCard = bonusCard ?? throw new ArgumentNullException(nameof(bonusCard));
        }

        public IBonusCard BonusCard { get; }

        public void Activate()
        {
            Activator.Activate();
        }
    }
}