using CannonTurret.Effects;
using System;
using UnityEngine;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class RocketBonusActivatorCreator : MonoBehaviour, IBonusActivatorCreator
    {
        [SerializeField] private BigBangBonusActivatorCreator _bigBangBonusActivatorCreator;
        [SerializeField] private RocketView _rocketView;

        private RocketBonusActivator _rocketBonusActivator;

        private void OnValidate()
        {
            if (_bigBangBonusActivatorCreator == null)
                throw new NullReferenceException(nameof(_bigBangBonusActivatorCreator));

            if (_rocketView == null)
                throw new NullReferenceException(nameof(_rocketView));
        }

        private void OnDisable()
        {
            _rocketBonusActivator.Disable();
        }

        public IBonusActivator Create()
        {
            if (_rocketBonusActivator == null)
            {
                var bigBangBonusActivator = _bigBangBonusActivatorCreator.Create();
                _rocketBonusActivator = new RocketBonusActivator(_rocketView, bigBangBonusActivator);
            }

            return _rocketBonusActivator;
        }
    }
}