using System;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Enemy attributes", fileName = "EnemyAttributes", order = 51)]
    public class EnemyAttributes : ScriptableObject, IDamageAttributes, IHealthImprover
    {
        private const float MinHealthCoefficient = 1f;
        private const float Abbreviation = 100f;

        [SerializeField] private DamageAttributes _damageAttributes;
        [SerializeField] private HealthAttributes _healthAttributes;

        [Header("Debug")]

        [SerializeField] private float _maxHealth;

        public float Damage => _damageAttributes.Damage;
        public float MaxHealth => _maxHealth;

        private void OnValidate()
        {
            if (_damageAttributes == null)
                throw new NullReferenceException(nameof(_damageAttributes));

            if (_healthAttributes == null)
                throw new NullReferenceException(nameof(_healthAttributes));
        }

        private void Awake()
        {
            _maxHealth = _healthAttributes.MaxHealth;
        }

        public void Improve(float healthCoefficient)
        {
            if (healthCoefficient < MinHealthCoefficient)
                throw new ArgumentOutOfRangeException($"The coefficient cannot be less than {MinHealthCoefficient}");

            _maxHealth = _healthAttributes.MaxHealth * healthCoefficient;
            _maxHealth = Mathf.Round(_maxHealth * Abbreviation) / Abbreviation;
        }
    }
}
