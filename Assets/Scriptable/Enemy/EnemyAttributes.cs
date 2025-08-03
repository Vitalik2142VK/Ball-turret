using System;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Enemy attributes", fileName = "EnemyAttributes", order = 51)]
    public class EnemyAttributes : ScriptableObject, IDamageAttributes, IHealthImprover
    {
        [SerializeField, Min(0f)] private float _damage;
        [SerializeField, Min(10f)] private float _maxHealth;

        private const float MinHealthCoefficient = 1f;
        private const float Abbreviation = 100f;

        public float Damage => _damage;
        public float MaxHealth => _maxHealth;

        public void Improve(float healthCoefficient)
        {
            if (healthCoefficient < MinHealthCoefficient)
                throw new ArgumentOutOfRangeException($"The coefficient cannot be less than {MinHealthCoefficient}");

            _maxHealth = _maxHealth * healthCoefficient;
            _maxHealth = Mathf.Round(_maxHealth * Abbreviation) / Abbreviation;
        }
    }
}
