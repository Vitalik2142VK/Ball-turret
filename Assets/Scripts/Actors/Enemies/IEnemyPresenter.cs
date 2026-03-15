using CannonTurret.DamageSystem;
using CannonTurret.Effects;

namespace CannonTurret.Actors.Enemies
{
    public interface IEnemyPresenter
    {
        public void PrepareDeleted(IRemovedActorsCollector removedCollector);

        public void PrepareAttacked(IAttackingEnemiesCollector attackingCollector);

        public void AddDebuff(IDebuff debaff);

        public void TakeDamage(IDamageAttributes damage);

        public void Move();

        public void Win();

        public void Destroy();
    }
}