using CannonTurret.DamageSystem;
using System;

namespace CannonTurret.HealthSystem
{
    public class Health : IHealth
    {
        private readonly IHealthAttributes Attributes;
        private readonly IHealthBarView HealthBar;

        private float _currentHealth;

        public Health(IHealthAttributes attributes, IHealthBarView healthBar)
        {
            Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
            HealthBar = healthBar ?? throw new ArgumentNullException(nameof(healthBar));
        }

        public bool IsAlive => _currentHealth > 0;

        public void Restore()
        {
            _currentHealth = Attributes.MaxHealth;
            HealthBar.SetMaxHealth(_currentHealth);
            HealthBar.SetActive(false);
        }

        public void TakeDamage(IDamageAttributes damage)
        {
            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            if (damage.Damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage.Damage));

            if (HealthBar.IsActive == false)
                HealthBar.SetActive(true);

            _currentHealth -= damage.Damage;

            if (IsAlive)
            {
                HealthBar.UpdateDataHealth(_currentHealth);
            }
            else
            {
                _currentHealth = 0;
                HealthBar.SetActive(false);
            }
        }
    }
}