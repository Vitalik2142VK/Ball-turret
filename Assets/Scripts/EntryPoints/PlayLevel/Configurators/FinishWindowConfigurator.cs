using CannonTurret.Coin.Rewards;
using CannonTurret.Coin.Wallets;
using CannonTurret.LevelSystem;
using CannonTurret.Scriptable.Level;
using CannonTurret.SDK.Ads;
using CannonTurret.StepSystem;
using CannonTurret.UI.PlayerScene;
using System;
using UnityEngine;

namespace CannonTurret.EntryPoints.PlayLevel.Configurators
{
    public class FinishWindowConfigurator : MonoBehaviour
    {
        [SerializeField] private FinishWindow _finishWindow;
        [SerializeField] private AddCoinsButton _addCoinsButton;
        [SerializeField] private NextLevelButton _nextLevelButton;
        [SerializeField] private ChangeSceneButton _mainMenuButton;
        [SerializeField] private RestartLevelButton _restartButton;
        [SerializeField] private LevelFactory _levelFactory;

        public FinishWindow FinishWindow => _finishWindow;

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

            if (_levelFactory == null)
                throw new NullReferenceException(nameof(_levelFactory));
        }

        public void Configure(
            ICoinAdder coinAdder,
            IRewardData rewardData,
            IAdsViewer adsViewer,
            IWinStatus winStatus,
            IChangeSceneStep changeSceneStep,
            ILevel currentLevel)
        {

            if (coinAdder == null)
                throw new ArgumentNullException(nameof(coinAdder));

            if (rewardData == null)
                throw new ArgumentNullException(nameof(rewardData));

            if (adsViewer == null)
                throw new ArgumentNullException(nameof(adsViewer));

            if (winStatus == null)
                throw new ArgumentNullException(nameof(winStatus));

            if (changeSceneStep == null)
                throw new ArgumentNullException(nameof(changeSceneStep));

            if (currentLevel == null)
                throw new ArgumentNullException(nameof(currentLevel));

            MainMenuLoader mainMenuLoader = new MainMenuLoader();

            _finishWindow.Initialize(rewardData, adsViewer, winStatus);
            _addCoinsButton.Initialize(coinAdder, adsViewer, RewardTypes.AddCoin);
            _nextLevelButton.Initialize(changeSceneStep, _levelFactory, currentLevel, winStatus);
            _mainMenuButton.Initialize(changeSceneStep, mainMenuLoader);
            _restartButton.Initialize(changeSceneStep, currentLevel);
        }
    }
}
