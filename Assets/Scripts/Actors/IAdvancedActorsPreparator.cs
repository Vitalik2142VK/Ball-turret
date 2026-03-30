using CannonTurret.Actors.Enemies;
using CannonTurret.Actors.MoveSystem;
using CannonTurret.LevelSystem;
using System.Collections.Generic;

namespace CannonTurret.Actors
{
    public interface IAdvancedActorsPreparator : IActorsPreparator
    {
        public IActorsMover ActorsMover { get; }

        public int EnemiesCount { get; }

        public IEnumerable<IActor> PopActors();

        public void CountRemainingEnemies();

        public void ActivateDebuffablies();

        public void SetLevel(ILevel level);

        public IEnumerable<IEnemy> GetEnemies();
    }
}