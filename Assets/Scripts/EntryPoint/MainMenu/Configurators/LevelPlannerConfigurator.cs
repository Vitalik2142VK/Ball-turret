using Scriptable;
using System;
using UnityEngine;

namespace MainMenuSpace
{
    public class LevelPlannerConfigurator : MonoBehaviour
    {
        [SerializeField] private PlaySceneLoader _sceneLoader;
        [SerializeField] private LevelActorsPlanner _learningLevelActorsPlanners;
        [SerializeField] private LevelActorsPlanner[] _levelActorsPlanners;

        [Header("Actors health coefficient by level")]
        [SerializeField, Range(0.3f, 2f)] private float _healthCoefficient;

        public ILevelFactory LevelFactory { get; private set; }
        public ICoinCountRandomizer CoinCountRandomizer { get; private set; }

        private void OnValidate()
        {
            if (_sceneLoader == null)
                throw new NullReferenceException(nameof(_sceneLoader));

            if (_learningLevelActorsPlanners == null)
                throw new NullReferenceException(nameof(_learningLevelActorsPlanners));

            if (_levelActorsPlanners == null || _levelActorsPlanners.Length == 0)
                throw new InvalidOperationException(nameof(_levelActorsPlanners));
        }

        public void Configure(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            CoinCountRandomizer = new CoinCountRandomizer(player.AchievedLevelIndex);
            LevelFactory = new LevelFactory(_levelActorsPlanners, CoinCountRandomizer, _healthCoefficient);
        }

        public void LoadLearningLevel()
        {
            float defaultHealthCoefficient = 1f;
            Level learningLevel = new Level(_learningLevelActorsPlanners, defaultHealthCoefficient);
            _sceneLoader.SetSelectedLevel(learningLevel);
            _sceneLoader.Load();
        }
    }
}
