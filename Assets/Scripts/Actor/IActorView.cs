public interface IActorView : IDestroyedObject
{
    public string Name { get; }

    public void PrepareDeleted(IRemovedActorsCollector removedCollector);
}