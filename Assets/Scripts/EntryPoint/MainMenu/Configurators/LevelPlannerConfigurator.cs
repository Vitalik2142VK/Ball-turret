using Scriptable;
using System;
using UnityEngine;

namespace MainMenuSpace
{
    public class LevelPlannerConfigurator : MonoBehaviour
    {
        [SerializeField] private PlaySceneLoader _sceneLoader;
        [SerializeField] private EndlessLevelPlanner _endlessLevelPlanner;
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

            if (_endlessLevelPlanner == null)
                throw new NullReferenceException(nameof(_endlessLevelPlanner));

            if (_learningLevelActorsPlanners == null)
                throw new NullReferenceException(nameof(_learningLevelActorsPlanners));

            if (_levelActorsPlanners == null || _levelActorsPlanners.Length == 0)
                throw new InvalidOperationException(nameof(_levelActorsPlanners));
        }

        public void Configure(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            _endlessLevelPlanner.Initialize();

            float coinsForRewardAdCoefficient = 3.5f;
            int achievedLevelIndex = player.AchievedLevelIndex;
            CoinCountRandomizer = new CoinCountRandomizer(achievedLevelIndex, coinsForRewardAdCoefficient);
            LevelFactory = new LevelFactory(_endlessLevelPlanner, _levelActorsPlanners, CoinCountRandomizer, _healthCoefficient, achievedLevelIndex);
        }

        public void LoadLearningLevel()
        {
            float defaultHealthCoefficient = 1f;
            Level learningLevel = new Level(_learningLevelActorsPlanners, CoinCountRandomizer, defaultHealthCoefficient);
            _sceneLoader.SetSelectedLevel(learningLevel);
            _sceneLoader.Load();
        }
    }
}
