using CannonTurret.Effects;
using System;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class RocketBonusActivator : IBonusActivator
    {
        private readonly IRocketView RocketView;
        private readonly IBonusActivator BigBangBonusActivator;

        private bool _isActivateStarted;

        public RocketBonusActivator(IRocketView rocketView, IBonusActivator bigBangBonusActivator)
        {
            RocketView = rocketView ?? throw new ArgumentNullException(nameof(rocketView));
            BigBangBonusActivator = bigBangBonusActivator ?? throw new ArgumentNullException(nameof(bigBangBonusActivator));
            _isActivateStarted = false;

            RocketView.RocketFinished += OnFinishRocket;
        }

        public void Activate()
        {
            if (_isActivateStarted)
                return;

            _isActivateStarted = true;
            RocketView.Play();
        }

        public void Disable()
        {
            RocketView.RocketFinished -= OnFinishRocket;
        }

        private void OnFinishRocket()
        {
            BigBangBonusActivator.Activate();
            _isActivateStarted = false;
        }
    }
}