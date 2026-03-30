using CannonTurret.PlayerSystem;
using UnityEngine;

namespace CannonTurret.Scriptable.Player
{
    [CreateAssetMenu(menuName = "Attributes/Improvement turret", fileName = "ImprovementTurretAttributes", order = 51)]
    public class ImprovementTurretAttributes : ScriptableObject, IImprovementTurretAttributes
    {
        [Header("Health")]
        [SerializeField][Min(1.5f)] private float _maxHealthCoefficient;
        [SerializeField][Min(0.1f)] private float _improveHealthCoefficient;

        [Header("Damege")]
        [SerializeField][Min(2f)] private float _maxDamageCoefficient;
        [SerializeField][Min(0.1f)] private float _improveDamageCoefficient;

        public float MaxHealthCoefficient => _maxHealthCoefficient;

        public float MaxDamageCoefficient => _maxDamageCoefficient;

        public float ImproveHealthCoefficient => _improveHealthCoefficient;

        public float ImproveDamageCoefficient => _improveDamageCoefficient;
    }
}
