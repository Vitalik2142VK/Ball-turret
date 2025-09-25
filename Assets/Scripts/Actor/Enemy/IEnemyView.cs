using System;

public interface IEnemyView : IActorView, IDamagedObject, IDebuffReceiver
{
    public void PlayDamage();

    public void PlayMovement(bool isMovinng);

    public void PlayDead();
}