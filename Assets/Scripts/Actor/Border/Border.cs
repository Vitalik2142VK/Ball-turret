using System;
using UnityEngine;

public class Border : IBorder
{
    private IBorderView _view;
    private IArmor _armor;
    private IHealth _health;

    public Border(IBorderView view, IArmor armor, IHealth health)
    {
        _view = view ?? throw new ArgumentNullException(nameof(view));
        _armor = armor ?? throw new ArgumentNullException(nameof(armor));
        _health = health ?? throw new ArgumentNullException(nameof(health));
    }

    public IMover Mover => _view.Mover;

    public bool IsEnable { get; private set; }

    /private void OnEnable()
    {
        IsEnable = true;

        _health.Restore();
    }

    /private void OnDisable()
    {
        IsEnable = false;
    }

    public void SetStartPosition(Vector3 startPosition) => _view.SetStartPosition(startPosition);

    public void Destroy() => _view.Destroy();

    public void TakeDamage(IDamageAttributes damage)
    {
        if (damage == null)
            throw new ArgumentNullException(nameof(damage));

        _armor.ReduceDamage(damage);

        if (_health.IsAlive == false)
            Destroy();
    }

    public void IgnoreArmor(IDamageAttributes damage)
    {
        if (damage == null)
            throw new ArgumentNullException(nameof(damage));

        _health.TakeDamage(damage);

        if (_health.IsAlive == false)
            Destroy();
    }
}
