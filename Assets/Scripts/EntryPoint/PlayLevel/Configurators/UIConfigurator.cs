using System;
using UnityEngine;

namespace PlayLevel
{
    public class UIConfigurator : MonoBehaviour
    {
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private FinishWindow _finishWindow;
        [SerializeField] private SettingMenu _settingMenu;
        [SerializeField] private AudioSetting _audioSetting;

        private AdsViewer _adsViewer;

        private void OnValidate()
        {
            if (_pauseMenu == null)
                throw new NullReferenceException(nameof(_pauseMenu));

            if (_finishWindow == null)
                throw new NullReferenceException(nameof(_finishWindow));

            if (_settingMenu == null)
                throw new NullReferenceException(nameof(_settingMenu));

            if (_audioSetting == null)
                throw new NullReferenceException(nameof(_audioSetting));
        }

        public void Configure(IStep closeSceneStep, IRewardIssuer reward)
        {
            if (closeSceneStep == null)
                throw new ArgumentNullException(nameof(closeSceneStep));

            if (reward == null)
                throw new ArgumentNullException(nameof(reward));

            _adsViewer = FindAnyObjectByType<AdsViewer>();

            if (_adsViewer == null)
                throw new NullReferenceException(nameof(_adsViewer));

            _pauseMenu.Initialize(closeSceneStep);
            _finishWindow.Initialize(reward, _adsViewer);
            _settingMenu.Initialize(_audioSetting);
        }
    }
}
