using System;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Player/Cached user", fileName = "CachedUser", order = 51)]
    public class CachedPlayer : ScriptableObject, IPlayer
    {
        private IPlayer _player;

        public IWallet Wallet => _player.Wallet;
        public ITurretImprover TurretImprover => _player.TurretImprover;
        public IPurchasesStorage PurchasesStorage => _player.PurchasesStorage;
        public float HealthCoefficient => _player.HealthCoefficient;
        public float DamageCoefficient => _player.DamageCoefficient;
        public int AchievedLevelIndex => _player.AchievedLevelIndex;
        public bool IsSaved => _player != null;

        public void SetUser(IPlayer player)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public void IncreaseAchievedLevel() => _player.IncreaseAchievedLevel();
    }
}
