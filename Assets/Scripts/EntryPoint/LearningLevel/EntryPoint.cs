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
        [SerializeField] private LearningFinishWindow _learningFinishWindow;

        private StepSystemConfigurator _stepSystemConfigurator;
        private ActorsConfigurator _actorsConfigurator;
        private AudioSetting _audioSetting;
        private OpenWindowButton _pauseButton;
        private FinishWindow _finishWindow;
        private IWinStatus _winStatus;

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

            if (_learningFinishWindow == null)
                throw new NullReferenceException(nameof(_learningFinishWindow));
        }

        private void Awake()
        {
            var playLevelConfigs = FindAnyObjectByType<PlayLevel.EntryPoint>().Configs;
            _audioSetting = FindAnyObjectByType<AudioSetting>();

            if (playLevelConfigs == null)
                throw new NullReferenceException(nameof(playLevelConfigs));

            if (_audioSetting == null)
                throw new NullReferenceException(nameof(_audioSetting));

            _stepSystemConfigurator = playLevelConfigs.StepSystemConfigurator;
            _actorsConfigurator = playLevelConfigs.ActorsConfigurator;
            _winStatus = playLevelConfigs.WinStatus;

            var configuratorUI = playLevelConfigs.UIConfigurator;
            _pauseButton = configuratorUI.PauseButton;

            var finishWindowConfigurator = playLevelConfigs.FinishWindowConfigurator;
            _finishWindow = finishWindowConfigurator.FinishWindow;
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
            _learningFinishWindow.Initialize(_finishWindow, _winStatus);
            _actorsConfigurator.AddActorFactory(_learningEnemyFactory);

            var changeSceneStep = _stepSystemConfigurator.ChangeSceneStep;
            LearningStep learningStep = new LearningStep(_learningUI, _selectedLevel);
            _stepSystemConfigurator.AddLearningStep(learningStep);
            _stepSystemConfigurator.ChangeFinishWindow(_learningFinishWindow);
            _pauseButton.SetPauseMenu(_pauseMenu);
            _pauseMenu.Initialize(changeSceneStep);
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
