public interface IBorderView : IActorView, IDamagedObject, IArmoredObject 
{
    public void PlayDamage();

    public void PlayDead();
}
