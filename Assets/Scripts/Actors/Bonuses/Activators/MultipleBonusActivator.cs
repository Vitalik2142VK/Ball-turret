using System;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class MultipleBonusActivator : IBonusActivator
    {
        private const int MinCountActivations = 2;

        private readonly IBonusActivator Activator;
        private readonly int CountActivations;

        public MultipleBonusActivator(IBonusActivator activator, int countActivations)
        {
            if (countActivations < MinCountActivations)
                throw new ArgumentOutOfRangeException($"The {nameof(countActivations)} must be greater than {MinCountActivations}");

            Activator = activator ?? throw new ArgumentNullException(nameof(activator));
            CountActivations = countActivations;
        }

        public void Activate()
        {
            for (int i = 0; i < CountActivations; i++)
                Activator.Activate();

        }
    }
}