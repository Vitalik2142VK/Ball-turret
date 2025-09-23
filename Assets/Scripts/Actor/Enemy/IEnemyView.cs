public interface IEnemyView : IActorView, IDamagedObject
{
    public void PlayDamage();

    public void PlayMovement(bool isMovinng);

    public void PlayDead();
}