using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [Serializable]
    public struct LineActorPlaner
    {
        private const int CountColumns = 3;

        [SerializeField] private GameObject[] _actors;

        public void Validate()
        {
            if (_actors == null || _actors.Length != CountColumns)
            {
                _actors = new GameObject[CountColumns];
            }
            else
            {
                if (IsValidGameObjects() == false)
                    throw new InvalidOperationException($"One or more objects do not contain a component <{nameof(IActorView)}>");
            }
        }

        public bool IsEmpty()
        {
            foreach (var actor in _actors)
                if (actor != null)
                    return false;

            return true;
        }

        public List<IActorPlanner> GetActorPlanners(int lineNumber)
        {
            if (IsEmpty())
                throw new NullReferenceException();

            List<IActorPlanner> actorPlanners = new List<IActorPlanner>();

            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i] != null)
                {
                    string nameActor = _actors[i].GetComponent<IActorView>().Name;
                    ActorPlanner actorPlanner = new ActorPlanner(nameActor, lineNumber, i);
                    actorPlanners.Add(actorPlanner);
                }
            }

            return actorPlanners;
        }

        private bool IsValidGameObjects()
        {
            foreach (var actor in _actors)
                if (actor != null)
                    if (actor.TryGetComponent(out IActorView _) == false)
                        return false;

            return true;
        }
    }
}
