using System;
using UnityEngine;

namespace PlayLevel
{
    public class UIConfigurator : MonoBehaviour
    {
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private FinishWindow _finishWindow;

        private void OnValidate()
        {
            if (_pauseMenu == null)
                throw new NullReferenceException(nameof(_pauseMenu));

            if (_finishWindow == null)
                throw new NullReferenceException(nameof(_finishWindow));
        }

        public void Configure(IStep closeSceneStep, IRewardIssuer reward)
        {
            if (closeSceneStep == null)
                throw new ArgumentNullException(nameof(closeSceneStep));

            if (reward == null)
                throw new ArgumentNullException(nameof(reward));

            _pauseMenu.Initialize(closeSceneStep);
            _finishWindow.Initialize(reward);
        }
    }
}
