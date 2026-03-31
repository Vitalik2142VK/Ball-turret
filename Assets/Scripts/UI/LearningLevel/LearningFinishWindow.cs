using CannonTurret.LevelSystem;
using CannonTurret.UI.PlayerScene;
using System;
using UnityEngine;

namespace CannonTurret.UI.LearningLevel
{
    public class LearningFinishWindow : MonoBehaviour, IWindow
    {
        private FinishWindow _finishWindow;
        private ChangeSceneButton[] _changeSceneButtons;
        private NextLevelButton _nextLevelButton;
        private RestartLevelButton _restartLevelButton;
        private IWinStatus _winStatus;

        public void Initialize(FinishWindow finishWindow, IWinStatus winStatus)
        {
            if (finishWindow == null)
                throw new ArgumentNullException(nameof(finishWindow));

            _finishWindow = finishWindow;
            _winStatus = winStatus ?? throw new ArgumentNullException(nameof(winStatus));

            _changeSceneButtons = _finishWindow.GetComponentsInChildren<ChangeSceneButton>();

            if (_changeSceneButtons == null || _changeSceneButtons.Length == 0)
                throw new InvalidOperationException($"{nameof(_finishWindow)} does not contain {nameof(ChangeSceneButton)}");

            foreach (var button in _changeSceneButtons)
            {
                if (button.TryGetComponent(out NextLevelButton nextLevelButton))
                    _nextLevelButton = nextLevelButton;
                else if (button.TryGetComponent(out RestartLevelButton restartLevelButton))
                    _restartLevelButton = restartLevelButton;
            }

            if (_nextLevelButton == null)
                throw new InvalidOperationException($"{nameof(_finishWindow)} does not contain {nameof(NextLevelButton)}");

            if (_restartLevelButton == null)
                throw new InvalidOperationException($"{nameof(_finishWindow)} does not contain {nameof(RestartLevelButton)}");
        }

        public void Enable()
        {
            _finishWindow.Enable();

            bool isWin = _winStatus.IsWin;

            foreach (var button in _changeSceneButtons)
                button.gameObject.SetActive(isWin);

            _restartLevelButton.gameObject.SetActive(isWin == false);
            _nextLevelButton.gameObject.SetActive(false);
        }
    }
}