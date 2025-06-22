using System;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Player/Cached user", fileName = "CachedUser", order = 51)]
    public class CachedPlayer : ScriptableObject, IPlayer
    {
        public IPlayer Player { get; private set; }

        public IWallet Wallet => Player.Wallet;
        public ITurretImprover TurretImprover => Player.TurretImprover;
        public float HealthCoefficient => Player.HealthCoefficient;
        public float DamageCoefficient => Player.DamageCoefficient;
        public int AchievedLevelIndex => Player.AchievedLevelIndex;
        public bool AreAdsDisabled => Player.AreAdsDisabled;
        public bool IsSaved => Player != null;

        public void SetUser(IPlayer player)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public void IncreaseAchievedLevel() => Player.IncreaseAchievedLevel();
    }
}
