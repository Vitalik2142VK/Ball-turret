using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Armor Attributes", fileName = "ArmorAttributes", order = 51)]
    public class ArmorAttributes : ScriptableObject, IArmorAttributes
    {
        private const float DefaultСoefficient = 1f;
        private const float MaxPersent = 90.0f;
        private const float Percent = 0.01f;

        [SerializeField, Range(0f, MaxPersent)] private float _armorPercent;

        public float DamageReductionCoefficient { get; private set; }

        public void CalculateArmor()
        {
            DamageReductionCoefficient = DefaultСoefficient - _armorPercent * Percent;
        }
    }
}
