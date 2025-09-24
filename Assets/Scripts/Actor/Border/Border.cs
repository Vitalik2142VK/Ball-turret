using System;
using UnityEngine;

public class Border : IBorder
{
    private IBorderView _view;
    private IMovableObject _mover;
    private IArmor _armor;
    private IHealth _health;

    public Border(IBorderView view, IMovableObject mover, IArmor armor, IHealth health)
    {
        _view = view ?? throw new ArgumentNullException(nameof(view));
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
        IsEnable = false;

        _view.Destroy();
    }

    private void Enable()
    {
        IsEnable = true;

        _health.Restore();
    }

    private void CheckAlive()
    {
        if (_health.IsAlive)
        {
            _view.PlayDamage();
        }
        else
        {
            IsEnable = false;

            Destroy();
        }
    }
}
