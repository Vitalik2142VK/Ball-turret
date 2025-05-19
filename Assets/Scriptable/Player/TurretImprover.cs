using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "User/Turret improver", fileName = "TurretImprover", order = 51)]
    public class TurretImprover : ScriptableObject, ITurretImprover
    {
        [SerializeField, Min(1f)] private float _maxHealthCoefficient;
        [SerializeField, Min(1f)] private float _maxDamageCoefficient;
        [SerializeField, Min(0.1f)] private float _improveHealthCoefficient;
        [SerializeField, Min(0.1f)] private float _improveDamageCoefficient;

        public float MaxHealthCoefficient => _maxDamageCoefficient;
        public float MaxDamageCoefficient => _maxDamageCoefficient;
        public float ImproveHealthCoefficient => _improveDamageCoefficient;
        public float ImproveDamageCoefficient => _improveDamageCoefficient;
    }
}
