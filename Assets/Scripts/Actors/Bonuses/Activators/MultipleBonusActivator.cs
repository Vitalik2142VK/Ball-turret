using System;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class MultipleBonusActivator : IBonusActivator
    {
        private const int MinCountActivations = 2;

        private readonly IBonusActivator _activator;
        private readonly int _countActivations;

        public MultipleBonusActivator(IBonusActivator activator, int countActivations)
        {
            if (countActivations < MinCountActivations)
            {
                string message = $"The {nameof(countActivations)} must be greater than {MinCountActivations}";

                throw new ArgumentOutOfRangeException(message);
            }

            _activator = activator ?? throw new ArgumentNullException(nameof(activator));
            _countActivations = countActivations;
        }

        public void Activate()
        {
            for (int i = 0; i < _countActivations; i++)
                _activator.Activate();

        }
    }
}