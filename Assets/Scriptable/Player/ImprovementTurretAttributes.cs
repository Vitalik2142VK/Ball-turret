using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Improvement turret", fileName = "ImprovementTurretAttributes", order = 51)]
    public class ImprovementTurretAttributes : ScriptableObject, IImprovementTurretAttributes
    {
        [Header("Health")]
        [SerializeField, Min(2f)] private float _maxHealthCoefficient;
        [SerializeField, Min(0.1f)] private float _improveHealthCoefficient;
        [SerializeField, Min(1)] private int _countLevelHealthImprovements;

        [Header("Damege")]
        [SerializeField, Min(2f)] private float _maxDamageCoefficient;
        [SerializeField, Min(0.1f)] private float _improveDamageCoefficient;
        [SerializeField, Min(1)] private int _countLevelDamageImprovements;

        private void OnValidate()
        {
            _countLevelHealthImprovements = Mathf.RoundToInt(_maxHealthCoefficient / _improveHealthCoefficient);
            _countLevelDamageImprovements = Mathf.RoundToInt(_maxDamageCoefficient / _improveDamageCoefficient);
        }

        public float MaxHealthCoefficient => _maxHealthCoefficient;
        public float MaxDamageCoefficient => _maxDamageCoefficient;
        public float ImproveHealthCoefficient => _improveHealthCoefficient;
        public float ImproveDamageCoefficient => _improveDamageCoefficient;
        public int CountLevelHealthImprovements => _countLevelHealthImprovements;
        public int CountLevelDamageImprovements => _countLevelDamageImprovements;
    }
}
