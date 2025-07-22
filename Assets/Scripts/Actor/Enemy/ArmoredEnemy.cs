using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class ArmoredEnemy : MonoBehaviour, IEnemy, IArmoredObject
{
    [SerializeField] private Scriptable.ArmorAttributes _armorAttributes;
    [SerializeField] private Enemy _enemy;

    private IArmor _armor;

    private void OnValidate()
    {
        if (_armorAttributes == null)
            throw new ArgumentNullException(nameof(_armorAttributes));

        if (TryGetComponent(out Enemy enemy))
            _enemy = enemy;
        else
            throw new NullReferenceException(nameof(_enemy));
    }

    public IMover Mover => _enemy.Mover;
    public string Name => _enemy.Name;
    public bool IsEnable => _enemy.IsEnable;

    private void Awake()
    {
        _armorAttributes.CalculateArmor();
        _armor = new Armor(_enemy, _armorAttributes);
    }

    public void ActivateDebuffs() => _enemy.ActivateDebuffs();

    public void AddDebuff(IDebuff debaff) => _enemy.AddDebuff(debaff);

    public void ApplyDamage(IDamagedObject damagedObject) => _enemy.ApplyDamage(damagedObject);

    public void Destroy() => _enemy.Destroy();

    public void SetStartPosition(Vector3 startPosition) => _enemy.SetStartPosition(startPosition);

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