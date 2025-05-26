using System;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Turret attributes", fileName = "TurretAttributes", order = 51)]
    public class TurretAttributes : ScriptableObject, IHealthAttributes, IDamageAttributes
    {
        [SerializeField] private DamageAttributes _bulletDamageAttributes;
        [SerializeField] private HealthAttributes _turretHealthAttributes;

        public float MaxHealth => _turretHealthAttributes.MaxHealth;
        public float Damage => _bulletDamageAttributes.Damage;

        private void OnValidate()
        {
            if (_bulletDamageAttributes == null)
                throw new NullReferenceException(nameof(_bulletDamageAttributes));

            if (_turretHealthAttributes == null)
                throw new NullReferenceException(nameof(_turretHealthAttributes));
        }
    }
}
