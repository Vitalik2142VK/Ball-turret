using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Damage Improver Attributes", fileName = "DamageImproverAttributes", order = 51)]
    public class DamageImproverAttributes : ScriptableObject, IDamageImproverAttributes
    {
        private const float DefaultСoefficient = 1f;
        private const float Percent = 0.01f;

        [SerializeField, Min(-99)] private int _percentageDamageIncrease;

        public float DamageСoefficient => DefaultСoefficient + _percentageDamageIncrease * Percent;
    }
}
