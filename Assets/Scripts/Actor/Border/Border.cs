using System;
using UnityEngine;

public class Border : IBorder
{
    private IBorderPresenter _presenter;
    private IMovableObject _mover;
    private IArmor _armor;
    private IHealth _health;

    public Border(IBorderPresenter presenter, IMovableObject mover, IArmor armor, IHealth health)
    {
        _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        _mover = mover ?? throw new ArgumentNullException(nameof(mover));
        _armor = armor ?? throw new ArgumentNullException(nameof(armor));
        _health = health ?? throw new ArgumentNullException(nameof(health));

        Enable();
    }

    public bool IsEnable { get; private set; }

    public bool IsFinished => _mover.IsFinished;

    public void SetStartPosition(Vector3 startPosition) => _mover.SetStartPosition(startPosition);

    public void SetPoint(Vector3 distance, float speed) => _mover.SetPoint(distance, speed);

    public void Move() => _mover.Move();

    public void TakeDamage(IDamageAttributes damage)
    {
        if (damage == null)
            throw new ArgumentNullException(nameof(damage));

        _armor.ReduceDamage(damage);

        CheckAlive();
    }

    public void IgnoreArmor(IDamageAttributes damage)
    {
        if (damage == null)
            throw new ArgumentNullException(nameof(damage));

        _health.TakeDamage(damage);

        CheckAlive();
    }

    public void Destroy()
    {
        _presenter.Destroy();
        IsEnable = false;
    }

    private void Enable()
    {
        IsEnable = true;

        _health.Restore();
    }

    private void CheckAlive()
    {
        if (_health.IsAlive == false)
            IsEnable = false;
    }
}
