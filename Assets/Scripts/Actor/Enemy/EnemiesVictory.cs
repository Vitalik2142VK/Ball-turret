using System;
using System.Collections.Generic;

public class EnemiesVictory : IEnemiesVictoryRepository
{
    private List<IEnemy> _enemies;
    private IWinStatus _winStatus;

    public EnemiesVictory(IWinStatus winStatus)
    {
        _enemies = new List<IEnemy>();
        _winStatus = winStatus;
    }


    public void SetEnemies(IEnumerable<IEnemy> enemies)
    {
        if (enemies == null)
            throw new ArgumentNullException(nameof(enemies));

        _enemies.Clear();
        _enemies.AddRange(enemies);
    }

    public void WinAll()
    {
        bool isWin = _winStatus.IsWin == false;

        if (isWin == false)
            return;

        foreach (var enemy in _enemies)
            enemy.Win(isWin);
    }
}