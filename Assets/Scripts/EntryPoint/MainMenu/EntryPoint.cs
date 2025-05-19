using System;
using UnityEngine;

namespace MainMenuSpace
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private UserConfigurator _userConfigurator;
        [SerializeField] private LevelPlannerConfigurator _levelsPlannerConfigurator;
        [SerializeField] private UIConfigurator _userInterfaseConfigurator;

        private void OnValidate()
        {
            if (_userConfigurator == null)
                throw new NullReferenceException(nameof(_userConfigurator));

            if (_levelsPlannerConfigurator == null)
                throw new NullReferenceException(nameof(_levelsPlannerConfigurator));

            if (_userInterfaseConfigurator == null)
                throw new NullReferenceException(nameof(_userInterfaseConfigurator));
        }

        private void Start()
        {
            _userConfigurator.Configure();
            _levelsPlannerConfigurator.Configure();

            var user = _userConfigurator.User;
            var levelFactory = _levelsPlannerConfigurator.LevelFactory;
            _userInterfaseConfigurator.Configure(user, levelFactory);
        }
    }
}
