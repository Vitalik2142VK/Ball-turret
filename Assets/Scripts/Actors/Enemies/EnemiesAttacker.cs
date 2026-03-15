using System;
using System.Collections.Generic;

public class EnemiesAttacker : IEnemiesAttacker, IAttackingEnemiesCollector
{
    private List<IEnemy> _attackingEnemies;
    private IDamagedObject _damagedObject;

    public EnemiesAttacker(IDamagedObject damagedObject)
    {
        _damagedObject = damagedObject ?? throw new ArgumentNullException(nameof(damagedObject));
        _attackingEnemies = new List<IEnemy>();
    }

    public void Add(IEnemy enemy)
    {
        if (enemy == null)
            throw new ArgumentNullException(nameof(enemy));

        _attackingEnemies.Add(enemy);
    }

    public void AttackAll()
    {
        foreach (var enemy in _attackingEnemies)
            enemy.ApplyDamage(_damagedObject);

        _attackingEnemies.Clear();
    }
}
