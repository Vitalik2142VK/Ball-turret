public interface IBorderView : IActorView, IDamagedObject, IArmoredObject 
{
    public bool IsActive { get; }

    public void PlayDamage();

    public void PlayDead();
}
