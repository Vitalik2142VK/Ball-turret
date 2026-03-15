using CannonTurret.AudioSystem;
using CannonTurret.LevelSystem;
using CannonTurret.StepSystem;
using CannonTurret.UI;
using CannonTurret.UI.PlayerScene;
using System;
using UnityEngine;

namespace CannonTurret.EntryPoints.PlayLevel.Configurators
{
    public class UIConfigurator : MonoBehaviour
    {
        [SerializeField] private Pause _pause;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private OpenWindowButton _pauseButton;
        [SerializeField] private SettingMenu _settingMenu;
        [SerializeField] private AudioSetting _audioSetting;
        [SerializeField] private BonusChoiceMenu _bonusChoiceMenu;
        [SerializeField] private ViewWavesCounter _viewWavesCounter;

        public OpenWindowButton PauseButton => _pauseButton;

        private void OnValidate()
        {
            if (_pause == null)
                throw new NullReferenceException(nameof(_pause));

            if (_pauseMenu == null)
                throw new NullReferenceException(nameof(_pauseMenu));

            if (_pauseButton == null)
                throw new NullReferenceException(nameof(_pauseButton));

            if (_settingMenu == null)
                throw new NullReferenceException(nameof(_settingMenu));

            if (_audioSetting == null)
                throw new NullReferenceException(nameof(_audioSetting));

            if (_bonusChoiceMenu == null)
                throw new NullReferenceException(nameof(_bonusChoiceMenu));

            if (_viewWavesCounter == null)
                throw new NullReferenceException(nameof(_viewWavesCounter));
        }

        public void Configure(IChangeSceneStep changeSceneStep, ISelectedLevel level)
        {
            if (changeSceneStep == null)
                throw new ArgumentNullException(nameof(changeSceneStep));

            if (level == null)
                throw new ArgumentNullException(nameof(level));

            level.SetViewWavesCounter(_viewWavesCounter);
            _pauseMenu.Initialize(changeSceneStep);
            _settingMenu.Initialize(_audioSetting);
            _pause.Initialize(_pauseButton);
            _bonusChoiceMenu.Initialize();
        }
    }
}
