using System;
using UnityEngine;

namespace MainMenuSpace
{
    public class LevelPlannerConfigurator : MonoBehaviour
    {
        [SerializeField] private Scriptable.LevelActorsPlanner[] _levelActorsPlanners;

        [Header("Actors health coefficient by level")]
        [SerializeField, Range(0.3f, 2f)] private float _healthCoefficient;

        public ILevelFactory LevelFactory { get; private set; }

        private void OnValidate()
        {
            if (_levelActorsPlanners == null)
                throw new NullReferenceException(nameof(_levelActorsPlanners));

            if (_levelActorsPlanners.Length == 0)
                throw new InvalidOperationException(nameof(_levelActorsPlanners));
        }

        public void Configure()
        {
            LevelFactory = new LevelFactory(_levelActorsPlanners, _healthCoefficient);
        }
    }
}
