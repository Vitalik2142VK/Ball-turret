public interface IBonusPresenter
{
    public void PrepareDeleted(IRemovedActorsCollector removedCollector);

    public void HandleBonusGatherer(IBonusGatherer bonusGathering);

    public void Destroy();
}
