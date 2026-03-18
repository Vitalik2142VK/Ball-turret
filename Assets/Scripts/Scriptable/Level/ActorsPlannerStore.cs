using CannonTurret.Actors.Spawn;
using CannonTurret.LevelSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CannonTurret.Scriptable.Level
{
    [CreateAssetMenu(menuName = "Level/Actors Planner Store", fileName = "ActorsPlannerStore", order = 51)]
    public class ActorsPlannerStore : ScriptableObject, IActorsPlannerStore
    {
        [SerializeField] private LevelActorsPlanner[] _levelActorsPlanners;

        private Dictionary<int, ILevelActorsPlanner> _actorsPlanners;

        public int LevelsCount => _actorsPlanners.Count;

        private void OnValidate()
        {
            if (_levelActorsPlanners == null || _levelActorsPlanners.Length == 0)
                throw new NullReferenceException(nameof(_levelActorsPlanners));

            foreach (var actorsPlanner in _levelActorsPlanners)
                if (actorsPlanner == null)
                    throw new NullReferenceException($"{_levelActorsPlanners} has null elements");
        }

        public void Initialize()
        {
            if (_actorsPlanners == null || _actorsPlanners.Count == 0)
                _actorsPlanners = new Dictionary<int, ILevelActorsPlanner>();
            else
                return;

            int index = EndlessLevel.IndexLevel;

            foreach (var actorsPlanner in _levelActorsPlanners)
                _actorsPlanners.Add(++index, actorsPlanner);
        }

        public bool HasIndex(int index)
        {
            return _actorsPlanners.ContainsKey(index);
        }

        public ILevelActorsPlanner GetLevelActorsPlanner(int index)
        {
            if (HasIndex(index) == false)
                throw new IndexOutOfRangeException(nameof(index));

            return _actorsPlanners[index];
        }
    }
}
