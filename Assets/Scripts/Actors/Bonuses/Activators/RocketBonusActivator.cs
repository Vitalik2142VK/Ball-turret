using CannonTurret.Effects;
using System;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class RocketBonusActivator : IBonusActivator
    {
        private readonly IRocketView _rocketView;
        private readonly IBonusActivator _bigBangBonusActivator;

        private bool _isActivateStarted;

        public RocketBonusActivator(IRocketView rocketView, IBonusActivator bigBangBonusActivator)
        {
            if (rocketView == null)
                throw new ArgumentNullException(nameof(rocketView));

            if (bigBangBonusActivator == null)
                throw new ArgumentNullException(nameof(bigBangBonusActivator));

            _rocketView = rocketView;
            _bigBangBonusActivator = bigBangBonusActivator;
            _isActivateStarted = false;

            _rocketView.RocketFinished += OnFinishRocket;
        }

        public void Activate()
        {
            if (_isActivateStarted)
                return;

            _isActivateStarted = true;
            _rocketView.Play();
        }

        public void Disable()
        {
            _rocketView.RocketFinished -= OnFinishRocket;
        }

        private void OnFinishRocket()
        {
            _bigBangBonusActivator.Activate();
            _isActivateStarted = false;
        }
    }
}