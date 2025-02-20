public interface IEnemy : IActor, IDamagedObject
{
    public void ApplyDamage(IDamagedObject damagedObject);
}