using System;
using UnityEngine;

public class ArmoredEnemy : IEnemy, IArmoredObject
{
    private IEnemy _enemy;
    private IArmor _armor;

    public ArmoredEnemy(IEnemy enemy, IArmor armor)
    {
        _enemy = enemy ?? throw new ArgumentNullException(nameof(enemy));
        _armor = armor ?? throw new ArgumentNullException(nameof(armor));
    }

    public bool IsEnable => _enemy.IsEnable;
    public bool IsFinished => _enemy.IsFinished;

    public void Enable() => _enemy.Enable();

    public void Disable() => _enemy.Disable();

    public void ActivateDebuffs() => _enemy.ActivateDebuffs();

    public void AddDebuff(IDebuff debaff) => _enemy.AddDebuff(debaff);

    public void ApplyDamage(IDamagedObject damagedObject) => _enemy.ApplyDamage(damagedObject);

    public void SetStartPosition(Vector3 startPosition) => _enemy.SetStartPosition(startPosition);

    public void SetPoint(Vector3 distance, float speed) => _enemy.SetPoint(distance, speed);

    public void Move() => _enemy.Move();

    public void Destroy() => _enemy.Destroy();

    public void TakeDamage(IDamageAttributes damage)
    {
        if (damage == null)
            throw new ArgumentNullException(nameof(damage));

        _armor.ReduceDamage(damage);
    }

    public void IgnoreArmor(IDamageAttributes attributes)
    {
        _enemy.TakeDamage(attributes);
    }
}
