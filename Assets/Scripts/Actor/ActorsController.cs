using System;

public class ActorsController : IActorsController, IActorsMover, IActorsRemover
{
    private IAdvancedActorsPreparator _actorsPreparator;
    private IRemovedActorsRepository _removedActorsRepository;
    private IActorsMover _actorsMover;

    public bool AreNoEnemies => _actorsPreparator.EnemiesCount == 0;
    public bool AreWavesOver => _actorsPreparator.AreWavesOver;
    public bool AreMovesFinished => _actorsMover.AreMovesFinished;

    public ActorsController(IAdvancedActorsPreparator actorsPreparator, IRemovedActorsRepository removedActorsRepository, IEnemiesAttacker enemiesAttacker)
    {
        _actorsPreparator = actorsPreparator ?? throw new ArgumentNullException(nameof(actorsPreparator));
        _removedActorsRepository = removedActorsRepository ?? throw new ArgumentNullException(nameof(removedActorsRepository));

        _actorsMover = _actorsPreparator.ActorsMover ?? throw new NullReferenceException(nameof(_actorsPreparator.ActorsMover));
    }

    public void MoveAll() => _actorsMover.MoveAll();

    public void Reboot()
    {
        var removedActors = _actorsPreparator.PopActors();
        _removedActorsRepository.AddRange(removedActors);
        _removedActorsRepository.RemoveAllDisabled();
    }

    public void Prepare()
    {
        if (_actorsPreparator.EnemiesCount > 0)
        {
            _actorsPreparator.ActivateDebuffablies();
            _actorsPreparator.CountRemainingEnemies();
        }
        
        if (_actorsPreparator.EnemiesCount == 0)
            Reboot();

        _actorsPreparator.Prepare();
    }

    public void RemoveAllDisabled()
    {
        _removedActorsRepository.RemoveAllDisabled();
        _actorsPreparator.CountRemainingEnemies();
    }
}
