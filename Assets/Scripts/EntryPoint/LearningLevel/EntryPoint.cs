using PlayLevel;
using System;
using UnityEngine;

namespace LearningLevel
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Scriptable.SelectedLevel _selectedLevel;
        [SerializeField] private LearningUI _learningUI;
        [SerializeField] private EnemyFactory _learningEnemyFactory;

        private StepSystemConfigurator _stepSystemConfigurator;
        private ActorsConfigurator _actorsConfigurator;

        private void OnValidate()
        {
            if (_selectedLevel == null)
                throw new NullReferenceException(nameof(_selectedLevel));

            if (_learningUI == null)
                throw new NullReferenceException(nameof(_learningUI));

            if (_learningEnemyFactory == null)
                throw new NullReferenceException(nameof(_learningEnemyFactory));
        }

        private void Awake()
        {
            _stepSystemConfigurator = FindAnyObjectByType<StepSystemConfigurator>();
            _actorsConfigurator = FindAnyObjectByType<ActorsConfigurator>();

            if (_stepSystemConfigurator == null)
                throw new NullReferenceException(nameof(_stepSystemConfigurator));

            if (_actorsConfigurator == null)
                throw new NullReferenceException(nameof(_actorsConfigurator));
        }

        private void Start()
        {
            //todo Remove ConfigureWithConsol() on realise
#if UNITY_EDITOR
            Configure();
#else
            ConfigureWithConsol();
#endif
        }

        private void Configure()
        {
            _learningEnemyFactory.Initialize(_selectedLevel);
            _actorsConfigurator.AddActorFactory(_learningEnemyFactory);

            LearningStep learningStep = new LearningStep(_learningUI, _selectedLevel);
            _stepSystemConfigurator.AddLearningStep(learningStep);
        }

        private void ConfigureWithConsol()
        {
            try
            {
                Configure();
            }
            catch (Exception ex)
            {
                Console.GetException(ex);
            }
        }
    }
}
