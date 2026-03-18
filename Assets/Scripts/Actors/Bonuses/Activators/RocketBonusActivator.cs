using CannonTurret.Effects;
using System;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class RocketBonusActivator : IBonusActivator
    {
        private IRocketView _rocketView;
        private IBonusActivator _bigBangBonusActivator;
        private bool _isActivateStarted;

        public RocketBonusActivator(IRocketView rocketView, IBonusActivator bigBangBonusActivator)
        {
            _rocketView = rocketView ?? throw new ArgumentNullException(nameof(rocketView));
            _bigBangBonusActivator = bigBangBonusActivator ?? throw new ArgumentNullException(nameof(bigBangBonusActivator));
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