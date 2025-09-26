public interface IEnemyPresenter
{
    public void AddDebuff(IDebuff debaff);

    public void TakeDamage(IDamageAttributes damage);

    public void Move();

    public void FinishDeath();

    public void Destroy();
}
