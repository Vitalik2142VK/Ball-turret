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

    public event Action Died;

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

        if (_currentHealth <= 0)
            Die();

        HealthBar.UpdateDataHealth(_currentHealth);
    }

    private void Die()
    {
        _currentHealth = 0;

        Died?.Invoke();
    }
}
