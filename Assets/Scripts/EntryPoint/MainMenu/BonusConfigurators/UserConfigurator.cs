using System;
using UnityEngine;
using Scriptable;

namespace MainMenuSpace
{
    public class UserConfigurator : MonoBehaviour
    {
        [SerializeField] private CachedUser _cachedUser;
        [SerializeField] private ImprovementTurretAttributes _improvementTurretAttributes;

        private IUserService _userService;

        public ITurretImprover TurretImprover => _cachedUser.TurretImprover;
        public IUser User => _cachedUser.User;

        private void OnValidate()
        {
            if (_cachedUser == null)
                throw new NullReferenceException(nameof(_cachedUser));

            if (_improvementTurretAttributes == null)
                throw new NullReferenceException(nameof(_improvementTurretAttributes));
        }

        private void Awake()
        {
            _userService = new CashUserService(_improvementTurretAttributes);
        }

        public void Configure()
        {
            if (_cachedUser.IsSaved)
            {
                _userService.Save(_cachedUser);
            }
            else
            {
                IUser user = _userService.Load();
                _cachedUser.SetUser(user);
            }
        }
    }
}
