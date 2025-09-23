using System;
using UnityEngine;

public class Enemy : IEnemy
{
    private IEnemyView _view;
    private IDebuffReceiver _debuffReceiver;
    private IMovableObject _mover;
    private IDamage _damage;
    private IHealth _health;

    public Enemy(IEnemyView enemyView, IDebuffReceiver debuffReceiver, IMovableObject mover, IDamage damage, IHealth health)
    {
        _view = enemyView ?? throw new ArgumentNullException(nameof(enemyView));
        _debuffReceiver = debuffReceiver ?? throw new ArgumentNullException(nameof(debuffReceiver));
        _mover = mover ?? throw new ArgumentNullException(nameof(mover));
        _damage = damage ?? throw new ArgumentNullException(nameof(damage));
        _health = health ?? throw new ArgumentNullException(nameof(health));
    }

    public bool IsFinished => _mover.IsFinished;

    public bool IsEnable { get; private set; }

    public void Enable()
    {
        IsEnable = true;

        _health?.Restore();
    }

    public void Disable()
    {
        IsEnable = false;
    }

    public void AddDebuff(IDebuff debaff) => _debuffReceiver.AddDebuff(debaff);

    public void ApplyDamage(IDamagedObject damagedObject) => _damage.Apply(damagedObject);

    public void SetStartPosition(Vector3 startPosition) => _mover.SetStartPosition(startPosition);

    public void SetPoint(Vector3 distance, float speed) => _mover.SetPoint(distance, speed);

    public void Move() 
    {
        _mover.Move();
        _view.PlayMovement(IsFinished == false);
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
            _view.PlayDamage();
        else
            _view.PlayDead();
    }

    public void Destroy()
    {
        _debuffReceiver.Clean();
        _view.Destroy();
    }
}
