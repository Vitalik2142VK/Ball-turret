using System;

public class ActorsController : IActorsController, IActorsMover, IActorsRemover, IEnemiesAttacker, IEnemiesVictory
{
    private IAdvancedActorPreparator _actorsPreparator;
    private IRemovedActorsRepository _removedActorsRepository;
    private IActorsMover _actorsMover;
    private IEnemiesAttacker _enemyAttacker;
    private IEnemiesVictoryRepository _enemiesVictory;

    public bool AreNoEnemies => _actorsPreparator.EnemiesCount == 0;
    public bool AreWavesOver => _actorsPreparator.AreWavesOver;
    public bool AreMovesFinished => _actorsMover.AreMovesFinished;

    public ActorsController(IAdvancedActorPreparator actorsPreparator, IRemovedActorsRepository removedActorsRepository, IEnemiesAttacker enemiesAttacker, IEnemiesVictoryRepository enemiesVictory)
    {
        _actorsPreparator = actorsPreparator ?? throw new ArgumentNullException(nameof(actorsPreparator));
        _removedActorsRepository = removedActorsRepository ?? throw new ArgumentNullException(nameof(removedActorsRepository));
        _enemyAttacker = enemiesAttacker ?? throw new ArgumentNullException(nameof(enemiesAttacker));
        _enemiesVictory = enemiesVictory ?? throw new ArgumentNullException(nameof(enemiesVictory));

        _actorsMover = _actorsPreparator.ActorsMover ?? throw new NullReferenceException(nameof(_actorsPreparator.ActorsMover));
    }

    public void Reboot()
    {
        var removedActors = _actorsPreparator.PopActors();
        _removedActorsRepository.AddRange(removedActors);
        _removedActorsRepository.RemoveAllDisabled();
    }

    public void Prepare()
    {
        if (AreNoEnemies == false)
        {
            _actorsPreparator.ActivateDebuffablies();
            _actorsPreparator.CountRemainingEnemies();
        }
        
        if (AreNoEnemies)
            Reboot();

        _actorsPreparator.Prepare();
    }

    public void Count()
    {
        _actorsPreparator.CountRemainingEnemies();
    }

    public void MoveAll()
    {
        _actorsMover.MoveAll();
    }

    public void RemoveAllDisabled()
    {
        _removedActorsRepository.RemoveAllDisabled();
        _actorsPreparator.CountRemainingEnemies();
    }

    public void AttackAll()
    {
        _enemyAttacker.AttackAll();
    }

    public void WinAll()
    {
        var enemies = _actorsPreparator.GetEnemies();
        _enemiesVictory.SetEnemies(enemies);
        _enemiesVictory.WinAll();
    }
}
