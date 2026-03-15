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
        _healthBar.SetActive(false);
    }

    public void TakeDamage(IDamageAttributes damage)
    {
        if (damage == null) 
            throw new ArgumentNullException(nameof(damage));

        if (damage.Damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage.Damage));

        if (_healthBar.IsActive == false)
            _healthBar.SetActive(true);

        _currentHealth -= damage.Damage;

        if (IsAlive)
        {
            _healthBar.UpdateDataHealth(_currentHealth);
        }
        else
        {
            _currentHealth = 0;
            _healthBar.SetActive(false);
        }
    }
}
