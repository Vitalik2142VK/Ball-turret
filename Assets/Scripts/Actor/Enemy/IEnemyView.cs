public interface IEnemyView : IActorView, IDamagedObject, IDebuffReceiver
{
    public void PrepareAttacked(IAttackingEnemiesCollector attackingCollector);

    public void PlayDamage();

    public void PlayMovement(bool isMovinng);

    public void PlayDead();
}