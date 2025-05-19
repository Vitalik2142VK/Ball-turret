using System;

public class Health : IHealth
{
    private IHealthAttributes _attributes;
    private IHealthBarView _healthBar;

    private float _currentHealth;

    public Health(IHealthAttributes attributes, IHealthBarView healthBar)
    {
        _attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
        _healthBar = healthBar ?? throw new ArgumentNullException(nameof(healthBar));
    }

    public bool IsAlive => _currentHealth > 0;

    public void Restore()
    {
        _currentHealth = _attributes.MaxHealth;
        _healthBar.SetMaxHealth(_currentHealth);
    }

    public void TakeDamage(IDamageAttributes damage)
    {
        if (damage == null) 
            throw new ArgumentNullException(nameof(damage));

        if (damage.Damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage.Damage));

        _currentHealth -= damage.Damage;

        if (IsAlive == false)
            _currentHealth = 0;

        _healthBar.UpdateDataHealth(_currentHealth);
    }
}
