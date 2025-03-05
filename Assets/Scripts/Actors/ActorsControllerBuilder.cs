public class ActorsControllerBuilder
{
    private ILevelActorsPlanner _levelActorsPlanner;
    private IActorSpawner _actorSpawner;
    private IActorsMover _actorsMover;
    private IActorsRemover _actorsRemover;
    private IEnemiesAttacker _enemiesAttacker;
    private IMoveAttributes _startMoveAttributes;
    private IMoveAttributes _defaultMoveAttributes;

    public ActorsControllerBuilder LevelActorsPlanner(ILevelActorsPlanner levelActorsPlanner)
    {
        _levelActorsPlanner = levelActorsPlanner;

        return this;
    }

    public ActorsControllerBuilder ActorSpawner(IActorSpawner actorSpawner)
    {
        _actorSpawner = actorSpawner;

        return this;
    }

    public ActorsControllerBuilder ActorsMover(IActorsMover actorsMover)
    {
        _actorsMover = actorsMover;

        return this;
    }

    public ActorsControllerBuilder ActorsRemover(IActorsRemover actorsRemover)
    {
        _actorsRemover = actorsRemover;

        return this;
    }

    public ActorsControllerBuilder EnemiesAttacker(IEnemiesAttacker enemiesAttacker)
    {
        _enemiesAttacker = enemiesAttacker;

        return this;
    }

    public ActorsControllerBuilder StartMoveAttributes(IMoveAttributes startMoveAttributes)
    {
        _startMoveAttributes = startMoveAttributes;

        return this;
    }

    public ActorsControllerBuilder DefaultMoveAttributes(IMoveAttributes defaultMoveAttributes)
    {
        _defaultMoveAttributes = defaultMoveAttributes;

        return this;
    }

    public ActorsController Build()
    {
        if (AreFieldsFill() == false)
            throw new System.InvalidOperationException();

        ActorsController actorsController = new ActorsController();
        actorsController.SetLevelActorsPlanner(_levelActorsPlanner);
        actorsController.SetActorSpawner(_actorSpawner);
        actorsController.SetActorsMover(_actorsMover, _startMoveAttributes, _defaultMoveAttributes);
        actorsController.SetActorsRemover(_actorsRemover);
        actorsController.SetEnemiesAttacker(_enemiesAttacker);

        return actorsController;
    }

    private bool AreFieldsFill()
    {
        return _levelActorsPlanner != null && _actorSpawner != null && _actorsMover != null && _actorsRemover != null && _enemiesAttacker != null &&
            _startMoveAttributes != null && _defaultMoveAttributes != null;
    }
}