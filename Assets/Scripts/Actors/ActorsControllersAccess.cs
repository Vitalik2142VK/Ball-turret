using System;

public class ActorsControllersAccess : IActorsControllersAccess
{
    public ActorsControllersAccess(IActorsController actorsController, IEnemiesController enemiesController)
    {
        ActorsController = actorsController ?? throw new ArgumentNullException(nameof(actorsController));
        EnemiesController = enemiesController ?? throw new ArgumentNullException(nameof(enemiesController));
    }

    public IActorsController ActorsController { get; }
    public IEnemiesController EnemiesController { get; }
}