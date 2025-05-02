using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scriptable
{
    [Serializable]
    public class LineActorPlaner
    {
        private const int CountColumns = 3;

        [SerializeField, SerializeIterface(typeof(IActor))] private GameObject[] _actors;

        public void Validate()
        {
            if (_actors == null || _actors.Length != CountColumns)
                _actors = new GameObject[CountColumns];
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
                    string nameActor = _actors[i].GetComponent<IActor>().Name;
                    ActorPlanner actorPlanner = new ActorPlanner(nameActor, lineNumber, i);
                    actorPlanners.Add(actorPlanner);
                }
            }

            return actorPlanners;
        }
    }
}
