using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Level/Wave Actors Planner", fileName = "WaveActorsPlanner", order = 51)]
    public class WaveActorsPlanner : ScriptableObject, IWaveActorsPlanner
    {
        private const int CountLines = 3;

        [SerializeField] private LineActorPlaner[] _lines;

        private void OnValidate()
        {
            if (_lines == null || _lines.Length != CountLines)
                _lines = new LineActorPlaner[CountLines];

            for (int i = 0; i < _lines.Length; i++)
            {
                ref var line = ref _lines[i];
                line.Validate();
            }

            if (IsEmpty())
                throw new InvalidOperationException($"There must be at least 1 enemy in the wave.");
        }

        public IReadOnlyCollection<IActorPlanner> GetActorPlanners()
        {
            List<IActorPlanner> planners = new List<IActorPlanner>();

            for (int i = 0; i < _lines.Length; i++)
            {
                if (_lines[i].IsEmpty() == false)
                {
                    List<IActorPlanner> actorPlanners = _lines[i].GetActorPlanners(i);
                    planners.AddRange(actorPlanners);
                }
            }

            return planners;
        }

        private bool IsEmpty()
        {
            foreach (var line in _lines)
                if (line.IsEmpty() == false)
                    return false;

            return true;
        }
    }
}
