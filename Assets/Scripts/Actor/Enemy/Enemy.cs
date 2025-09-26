using System;
using UnityEngine;

public class Enemy : IEnemy
{
    private IEnemyPresenter _presenter;
    private IDebuffHandler _debuffReceiver;
    private IMovableObject _mover;
    private IDamage _damage;
    private IHealth _health;

    public Enemy(IEnemyPresenter presenter, IDebuffHandler debuffReceiver, IMovableObject mover, IDamage damage, IHealth health)
    {
        _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        _debuffReceiver = debuffReceiver ?? throw new ArgumentNullException(nameof(debuffReceiver));
        _mover = mover ?? throw new ArgumentNullException(nameof(mover));
        _damage = damage ?? throw new ArgumentNullException(nameof(damage));
        _health = health ?? throw new ArgumentNullException(nameof(health));

        Enable();
    }

    public bool IsFinished => _mover.IsFinished;

    public bool IsEnable { get; private set; }

    public void AddDebuff(IDebuff debaff) => _debuffReceiver.AddDebuff(debaff);

    public void ApplyDamage(IDamagedObject damagedObject) => _damage.Apply(damagedObject);

    public void SetStartPosition(Vector3 startPosition) => _mover.SetStartPosition(startPosition);

    public void SetPoint(Vector3 distance, float speed) => _mover.SetPoint(distance, speed);

    public void Move() 
    {
        _mover.Move();
        _presenter.Move();
    }

    public void ActivateDebuffs()
    {
        _debuffReceiver.ActivateDebuffs();
        _debuffReceiver.RemoveCompletedDebuffs();
    }

    public void TakeDamage(IDamageAttributes damage)
    {
        _health.TakeDamage(damage);

        if (_health.IsAlive == false)
            IsEnable = false;
    }

    public void Destroy()
    {
        _debuffReceiver.Clean();
        _presenter.Destroy();
        IsEnable = false;
    }

    private void Enable()
    {
        IsEnable = true;
        _health.Restore();
    }
}
