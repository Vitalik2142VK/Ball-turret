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
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private Pause _pause;
        [SerializeField] private SettingMenu _settingMenu;

        private StepSystemConfigurator _stepSystemConfigurator;
        private ActorsConfigurator _actorsConfigurator;
        private PauseButton _pauseButton;
        private AudioSetting _audioSetting;

        private void OnValidate()
        {
            if (_selectedLevel == null)
                throw new NullReferenceException(nameof(_selectedLevel));

            if (_learningUI == null)
                throw new NullReferenceException(nameof(_learningUI));

            if (_learningEnemyFactory == null)
                throw new NullReferenceException(nameof(_learningEnemyFactory));

            if (_pauseMenu == null)
                throw new NullReferenceException(nameof(_pauseMenu));

            if (_pause == null)
                throw new NullReferenceException(nameof(_pause));

            if (_settingMenu == null)
                throw new NullReferenceException(nameof(_settingMenu));
        }

        private void Awake()
        {
            _stepSystemConfigurator = FindAnyObjectByType<StepSystemConfigurator>();
            _actorsConfigurator = FindAnyObjectByType<ActorsConfigurator>();
            _pauseButton = FindAnyObjectByType<PauseButton>();
            _audioSetting = FindAnyObjectByType<AudioSetting>();

            if (_stepSystemConfigurator == null)
                throw new NullReferenceException(nameof(_stepSystemConfigurator));

            if (_actorsConfigurator == null)
                throw new NullReferenceException(nameof(_actorsConfigurator));

            if (_pauseButton == null)
                throw new NullReferenceException(nameof(_pauseButton));

            if (_audioSetting == null)
                throw new NullReferenceException(nameof(_audioSetting));
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

            var closeSceneStep = _stepSystemConfigurator.CloseSceneStep;
            LearningStep learningStep = new LearningStep(_learningUI, _selectedLevel);
            _stepSystemConfigurator.AddLearningStep(learningStep);
            _pauseButton.SetPauseMenu(_pauseMenu);
            _pauseMenu.Initialize(closeSceneStep);
            _pause.Initialize(_pauseButton);
            _settingMenu.Initialize(_audioSetting);
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
