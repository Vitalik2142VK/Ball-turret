using System;
using UnityEngine;
using Scriptable;

namespace MainMenuSpace
{
    public class PlayerConfigurator : MonoBehaviour
    {
        [SerializeField] private CachedPlayer _cachedUser;
        [SerializeField] private ImprovementTurretAttributes _improvementTurretAttributes;

        private IPlayerService _playerService;

        public ITurretImprover TurretImprover => _cachedUser.TurretImprover;
        public IPlayer Player => _cachedUser.Player;

        private void OnValidate()
        {
            if (_cachedUser == null)
                throw new NullReferenceException(nameof(_cachedUser));

            if (_improvementTurretAttributes == null)
                throw new NullReferenceException(nameof(_improvementTurretAttributes));
        }

        private void Awake()
        {
            _playerService = new CashPlayerService(_improvementTurretAttributes);
        }

        public void Configure()
        {
            if (_cachedUser.IsSaved)
            {
                _playerService.Save(_cachedUser);
            }
            else
            {
                IPlayer player = _playerService.Load();
                _cachedUser.SetUser(player);
            }
        }
    }
}
