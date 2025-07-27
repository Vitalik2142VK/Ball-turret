using PlayLevel;
using System;
using UnityEngine;

namespace LearningLevel
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Scriptable.SelectedLevel _selectedLevel;
        [SerializeField] private LearningUI _learningUI;

        private StepSystemConfigurator _stepSystemConfigurator;

        private void OnValidate()
        {
            if (_selectedLevel == null)
                throw new NullReferenceException(nameof(_selectedLevel));

            if (_learningUI == null)
                throw new NullReferenceException(nameof(_learningUI));
        }

        private void Awake()
        {
            _stepSystemConfigurator = FindAnyObjectByType<StepSystemConfigurator>();

            if (_stepSystemConfigurator == null)
                throw new NullReferenceException(nameof(_stepSystemConfigurator));
        }

        private void Start()
        {
            // Todo Remove ConfigureWithConsol() on realise
#if UNITY_EDITOR
            Configure();
#else
            ConfigureWithConsol();
#endif
        }

        private void Configure()
        {
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
