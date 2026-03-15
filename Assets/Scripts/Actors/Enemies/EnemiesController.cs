using System;

public class EnemiesController : IEnemiesController
{
    private IAdvancedActorsPreparator _actorsPreparator;
    private IEnemiesAttacker _enemyAttacker;

    public EnemiesController(IAdvancedActorsPreparator actorsPreparator, IEnemiesAttacker enemyAttacker)
    {
        _actorsPreparator = actorsPreparator ?? throw new ArgumentNullException(nameof(actorsPreparator));
        _enemyAttacker = enemyAttacker ?? throw new ArgumentNullException(nameof(enemyAttacker));
    }

    public bool AreNoEnemies => _actorsPreparator.EnemiesCount == 0;

    public void AttackAll() => _enemyAttacker.AttackAll();
        
    public void Count() => _actorsPreparator.CountRemainingEnemies();

    public void WinAll()
    {
        var enemies = _actorsPreparator.GetEnemies();
        
        foreach (var enemy in enemies)
            enemy.Win();
    }
}