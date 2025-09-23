public interface IEnemy : IActor, IActorModel, IDamagedObject, IDebuffable
{
    public void ApplyDamage(IDamagedObject damagedObject);
}