using System;
using UnityEngine;

namespace PlayLevel
{
    public class FinishWindowConfigurator : MonoBehaviour
    {
        [SerializeField] private FinishWindow _finishWindow;
        [SerializeField] private AddCoinsButton _addCoinsButton;
        [SerializeField] private NextLevelButton _nextLevelButton;
        [SerializeField] private MainMenuButton _mainMenuButton;
        [SerializeField] private RestartButton _restartButton;

        private void OnValidate()
        {
            if (_finishWindow == null)
                throw new NullReferenceException(nameof(_finishWindow));

            if (_addCoinsButton == null)
                throw new NullReferenceException(nameof(_addCoinsButton));

            if (_nextLevelButton == null)
                throw new NullReferenceException(nameof(_nextLevelButton));

            if (_mainMenuButton == null)
                throw new NullReferenceException(nameof(_mainMenuButton));

            if (_restartButton == null)
                throw new NullReferenceException(nameof(_restartButton));
        }

        public void Configure(ICoinAdder coinAdder, IRewardData rewardData, IAdsViewer adsViewer, IWinStatus winStatus)
        {

            if (coinAdder == null)
                throw new ArgumentNullException(nameof(coinAdder));

            if (rewardData == null)
                throw new ArgumentNullException(nameof(rewardData));

            if (adsViewer == null)
                throw new ArgumentNullException(nameof(adsViewer));

            if (winStatus == null)
                throw new ArgumentNullException(nameof(winStatus));

            _finishWindow.Initialize(rewardData, adsViewer, winStatus);
            _addCoinsButton.Initialize(coinAdder, adsViewer, RewardTypes.AddCoin);
        }
    }
}
