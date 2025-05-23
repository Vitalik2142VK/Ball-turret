using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "User/Cached user", fileName = "CachedUser", order = 51)]
    public class CachedUser : ScriptableObject, IUser
    {
        public IUser User { get; private set; }

        public IWallet Wallet => User.Wallet;
        public ITurretImprover TurretImprover => User.TurretImprover;
        public float HealthCoefficient => User.HealthCoefficient;
        public float DamageCoefficient => User.DamageCoefficient;
        public int AchievedLevelIndex => User.AchievedLevelIndex;
        public bool AreAdsDisabled => User.AreAdsDisabled;
        public bool IsSaved => User != null;

        public void SetUser(IUser user)
        {
            User = user ?? throw new System.ArgumentNullException(nameof(user));
        }

        public void IncreaseAchievedLevel() => User.IncreaseAchievedLevel();
    }
}
