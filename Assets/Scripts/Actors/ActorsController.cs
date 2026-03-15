using System;

public class ActorsController : IActorsController
{
    private IAdvancedActorsPreparator _actorsPreparator;
    private IRemovedActorsRepository _removedActorsRepository;
    private IActorsMover _actorsMover;

    public bool AreMovesFinished => _actorsMover.AreMovesFinished;

    public ActorsController(IAdvancedActorsPreparator actorsPreparator, IRemovedActorsRepository removedActorsRepository)
    {
        _actorsPreparator = actorsPreparator ?? throw new ArgumentNullException(nameof(actorsPreparator));
        _removedActorsRepository = removedActorsRepository ?? throw new ArgumentNullException(nameof(removedActorsRepository));

        _actorsMover = _actorsPreparator.ActorsMover ?? throw new NullReferenceException(nameof(_actorsPreparator.ActorsMover));
    }

    public void MoveAll() => _actorsMover.MoveAll();

    public void Prepare()
    {
        if (_actorsPreparator.EnemiesCount > 0)
        {
            _actorsPreparator.ActivateDebuffablies();
            _actorsPreparator.CountRemainingEnemies();
        }
        
        if (_actorsPreparator.EnemiesCount == 0)
            RemoveAll();

        _actorsPreparator.Prepare();
    }

    public void RemoveAll()
    {
        var removedActors = _actorsPreparator.PopActors();
        _removedActorsRepository.AddRange(removedActors);
        _removedActorsRepository.RemoveAll();
    }

    public void RemoveAllDisabled()
    {
        _removedActorsRepository.RemoveAll();
        _actorsPreparator.CountRemainingEnemies();
    }
}
