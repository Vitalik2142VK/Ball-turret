using System.Collections.Generic;

public interface IEnemiesVictoryRepository : IEnemiesVictory
{
    public void SetEnemies(IEnumerable<IEnemy> enemies);
}
