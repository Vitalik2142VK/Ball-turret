using System;

public class Health : IHealth
{
    private readonly IHealthAttributes Attributes;
    private readonly IHealthBarView HealthBar;

    private int _currentHealth;

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
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _currentHealth -= damage;

        if (IsAlive == false)
            _currentHealth = 0;

        HealthBar.UpdateDataHealth(_currentHealth);
    }
}
