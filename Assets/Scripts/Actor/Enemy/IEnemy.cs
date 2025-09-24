public interface IEnemy : IActor, IDamagedObject, IDebuffable
{
    public void ApplyDamage(IDamagedObject damagedObject);
}