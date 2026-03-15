using CannonTurret.DamageSystem;
using CannonTurret.HealthSystem;
using CannonTurret.Scriptable.Health;
using System;
using UnityEngine;

namespace CannonTurret.Scriptable.Player
{
    [CreateAssetMenu(menuName = "Attributes/Turret attributes", fileName = "TurretAttributes", order = 51)]
    public class TurretAttributes : ScriptableObject, IHealthAttributes, IDamageAttributes
    {
        [SerializeField] private Damage.DamageAttributes _bulletDamageAttributes;
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
