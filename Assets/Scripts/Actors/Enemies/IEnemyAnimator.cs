using CannonTurret.DamageSystem;

namespace CannonTurret.Actors.Enemies
{
    public interface IEnemyAnimator : IDamagedObjectAnimator
    {
        public void PlayMovement(bool isRunning);

        public void PlayVictory();
    }
}