using CannonTurret.Turrets;

namespace CannonTurret.PlayerSystem
{
    public interface IPlayerController
    {
        public void Initialize(ITurret turret);

        public void SelectTarget();
    }
}