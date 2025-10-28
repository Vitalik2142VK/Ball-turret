using System;
using UnityEngine;

namespace PlayLevel
{
    public class UIConfigurator : MonoBehaviour
    {
        [SerializeField] private Pause _pause;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private OpenWindowButton _pauseButton;
        [SerializeField] private FinishWindow _finishWindow;
        [SerializeField] private SettingMenu _settingMenu;
        [SerializeField] private AudioSetting _audioSetting;
        [SerializeField] private BonusChoiceMenu _bonusChoiceMenu;
        [SerializeField] private AddCoinsButton _addCoinsButton;

        public OpenWindowButton PauseButton => _pauseButton;

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

            if (_bonusChoiceMenu == null)
                throw new NullReferenceException(nameof(_bonusChoiceMenu));

            if (_addCoinsButton == null)
                throw new NullReferenceException(nameof(_addCoinsButton));
        }

        public void Configure(IStep closeSceneStep, IRewardIssuer reward, IWinStatus winStatus, ICoinAdder coinAdder, IAdsViewer adsViewer)
        {
            if (closeSceneStep == null)
                throw new ArgumentNullException(nameof(closeSceneStep));

            if (reward == null)
                throw new ArgumentNullException(nameof(reward));

            if (winStatus == null)
                throw new ArgumentNullException(nameof(winStatus));

            if (coinAdder == null)
                throw new ArgumentNullException(nameof(coinAdder));

            if (adsViewer == null)
                throw new ArgumentNullException(nameof(adsViewer));

            _pauseMenu.Initialize(closeSceneStep);
            _finishWindow.Initialize(reward, adsViewer, winStatus);
            _settingMenu.Initialize(_audioSetting);
            _pause.Initialize(_pauseButton);
            _bonusChoiceMenu.Initialize();
            _addCoinsButton.Initialize(coinAdder, adsViewer);

            var adsViewButton = _addCoinsButton.GetComponent<AdsViewButton>();
            adsViewButton.Initialize(adsViewer, RewardTypes.AddCoin);
        }
    }
}
