using System;
using UnityEngine;

namespace PlayLevel
{
    public class UIConfigurator : MonoBehaviour
    {
        [SerializeField] private PauseMenu _pauseMenu;

        private void OnValidate()
        {
            if (_pauseMenu == null)
                throw new NullReferenceException(nameof(_pauseMenu));
        }

        public void Configure(CloseSceneStep closeSceneStep)
        {
            if (closeSceneStep == null)
                throw new ArgumentNullException(nameof(closeSceneStep));

            _pauseMenu.Initialize(closeSceneStep);
        }
    }
}
