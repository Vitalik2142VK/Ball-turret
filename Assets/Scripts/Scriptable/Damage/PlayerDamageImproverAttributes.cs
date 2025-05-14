using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Player Damage Improver Attributes", fileName = "PlayerDamageImproverAttributes", order = 51)]
    public class PlayerDamageImproverAttributes : ScriptableObject, IDamageImproverAttributes
    {
        private const float DefaultСoefficient = 1f;
        private const float MinPersent = -99.9f;
        private const float Percent = 0.01f;

        private float _percentageDamageIncrease = 0f;

        public float DamageСoefficient => DefaultСoefficient + _percentageDamageIncrease * Percent;

        public void SetPercentageDamageIncrease(float percentageDamageIncrease = 0)
        {
            if (percentageDamageIncrease < MinPersent)
                throw new System.ArgumentOutOfRangeException($"{nameof(percentageDamageIncrease)} cannot be less than {MinPersent}");

            _percentageDamageIncrease = percentageDamageIncrease;
        }
    }
}
