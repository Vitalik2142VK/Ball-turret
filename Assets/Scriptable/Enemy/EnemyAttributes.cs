using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Enemy attributes", fileName = "EnemyAttributes", order = 51)]
    public class EnemyAttributes : ScriptableObject, IDamageAttributes, IHealthAttributes
    {
        [SerializeField, Min(0f)] private float _damage;
        [SerializeField, Min(10f)] private float _maxHealth;

        public float Damage => _damage;
        public float MaxHealth => _maxHealth;
    }
}
