using CannonTurret.StepSystem;
using CannonTurret.StepSystem.Steps;
using System;
using UnityEngine;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class FreezingBonusActivatorCreator : MonoBehaviour, IBonusActivatorCreator
    {
        [SerializeField] private FreezingView _freezingView;

        private IDynamicEndStep _nextStepPrepareActors;
        private ActorsFreezeStep _freezeStep;

        private void OnValidate()
        {
            if (_freezingView == null)
                throw new NullReferenceException(nameof(_freezingView));
        }

        public void Initialize(IDynamicEndStep nextStepPrepareActors, ActorsFreezeStep freezeStep)
        {
            if (nextStepPrepareActors == null)
                throw new NullReferenceException(nameof(nextStepPrepareActors));

            if (freezeStep == null)
                throw new NullReferenceException(nameof(freezeStep));

            _nextStepPrepareActors = nextStepPrepareActors;
            _freezeStep = freezeStep;
        }

        public IBonusActivator Create()
        {
            return new FreezingBonusActivator(_nextStepPrepareActors, _freezingView, _freezeStep);
        }
    }
}