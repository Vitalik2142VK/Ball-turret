public interface IBorderPresenter
{
    public void PrepareDeleted(IRemovedActorsCollector removedCollector);

    public void TakeDamage(IDamageAttributes damage);

    public void IgnoreArmor(IDamageAttributes damage);

    public void FinishDeath();

    public void Destroy();
}
