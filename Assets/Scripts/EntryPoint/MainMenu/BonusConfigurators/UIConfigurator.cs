using System;
using UnityEngine;

namespace MainMenuSpace
{
    public class UIConfigurator : MonoBehaviour
    {
        [SerializeField] private PlaySceneLoader _sceneLoader;
        [SerializeField] private PlayMenu _playMenu;

        private void OnValidate()
        {
            if (_sceneLoader == null)
                throw new NullReferenceException(nameof(_sceneLoader));

            if (_playMenu == null)
                throw new NullReferenceException(nameof(_playMenu));
        }

        public void Configure(IUser user, ILevelFactory levelFactory)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (levelFactory == null)
                throw new ArgumentNullException(nameof(levelFactory));

            _playMenu.Initialize(user, levelFactory, _sceneLoader);
        }
    }
}
