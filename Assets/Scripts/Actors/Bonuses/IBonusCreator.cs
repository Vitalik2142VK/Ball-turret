using CannonTurret.Actors.Bonuses.Activators;

namespace CannonTurret.Actors.Bonuses
{
    public interface IBonusCreator
    {
        public string Name { get; }

        public void Initialize(IBonusActivator bonusActivator);

        public IBonus Create();
    }
}