using System;
using UnityEngine;
using Scriptable;

namespace MainMenuSpace
{
    public class UserConfigurator : MonoBehaviour
    {
        [SerializeField] private CachedUser _cachedUser;
        [SerializeField] private TurretImprover _turretImprover;

        private IUserService _userService;

        public IUser User => _cachedUser.User;

        private void OnValidate()
        {
            if (_cachedUser == null)
                throw new NullReferenceException(nameof(_cachedUser));

            if (_turretImprover == null)
                throw new NullReferenceException(nameof(_turretImprover));
        }

        private void Awake()
        {
            _userService = new CashUserService(_turretImprover);
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
