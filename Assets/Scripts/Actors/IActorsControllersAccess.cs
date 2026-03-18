using CannonTurret.Actors.Enemies;

namespace CannonTurret.Actors
{
    public interface IActorsControllersAccess
    {
        public IActorsController ActorsController { get; }
        public IEnemiesController EnemiesController { get; }
    }
}