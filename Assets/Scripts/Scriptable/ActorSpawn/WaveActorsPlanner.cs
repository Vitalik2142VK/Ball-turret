using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Level Actors Planner/Wave Actors Planner", fileName = "WaveActorsPlanner", order = 51)]
    public class WaveActorsPlanner : ScriptableObject, IWaveActorsPlanner
    {
        private const int MaxActors = 9;

        [SerializeField] private ActorPlanner[] _actorPlanners;

        private void OnValidate()
        {
            if (_actorPlanners.Length > MaxActors)
                throw new InvalidOperationException($"There can be no more than {MaxActors} actors in one wave.");
        }

        public IReadOnlyCollection<IActorPlanner> GetActorPlanners()
        {
            return new List<IActorPlanner>(_actorPlanners);
        }
    }
}
