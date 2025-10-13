using System;
using UnityEngine;

namespace PlayLevel
{
    public class UIConfigurator : MonoBehaviour
    {
        [SerializeField] private Pause _pause;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private FinishWindow _finishWindow;
        [SerializeField] private SettingMenu _settingMenu;
        [SerializeField] private AudioSetting _audioSetting;

        private AdsViewer _adsViewer;

        private void OnValidate()
        {
            if (_pause == null)
                throw new NullReferenceException(nameof(_pause));

            if (_pauseMenu == null)
                throw new NullReferenceException(nameof(_pauseMenu));

            if (_pauseButton == null)
                throw new NullReferenceException(nameof(_pauseButton));

            if (_finishWindow == null)
                throw new NullReferenceException(nameof(_finishWindow));

            if (_settingMenu == null)
                throw new NullReferenceException(nameof(_settingMenu));

            if (_audioSetting == null)
                throw new NullReferenceException(nameof(_audioSetting));
        }

        public void Configure(IStep closeSceneStep, IRewardIssuer reward, IWinStatus winStatus)
        {
            if (closeSceneStep == null)
                throw new ArgumentNullException(nameof(closeSceneStep));

            if (reward == null)
                throw new ArgumentNullException(nameof(reward));

            if (winStatus == null)
                throw new ArgumentNullException(nameof(winStatus));

            _adsViewer = FindAnyObjectByType<AdsViewer>();

            if (_adsViewer == null)
                throw new NullReferenceException(nameof(_adsViewer));

            _pauseMenu.Initialize(closeSceneStep);
            _finishWindow.Initialize(reward, _adsViewer, winStatus);
            _settingMenu.Initialize(_audioSetting);
            _pause.Initialize(_pauseButton);
        }
    }
}
