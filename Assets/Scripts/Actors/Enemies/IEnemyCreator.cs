using CannonTurret.HealthSystem;

namespace CannonTurret.Actors.Enemies
{
    public interface IEnemyCreator
    {
        public string Name { get; }

        public IEnemy Create(IActorHealthModifier healthModifier);
    }
}