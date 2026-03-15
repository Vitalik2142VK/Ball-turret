using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Damage improver attributes", fileName = "DamageImproverAttributes", order = 51)]
    public class DamageImproverAttributes : ScriptableObject, IDamageImproverAttributes
    {
        private const float DefaultСoefficient = 1f;
        private const float MinPersent = -99.9f;
        private const float Percent = 0.01f;

        [SerializeField, Min(MinPersent)] private float _percentageDamageIncrease = 0f;

        public float DamageСoefficient => DefaultСoefficient + _percentageDamageIncrease * Percent;
    }
}
