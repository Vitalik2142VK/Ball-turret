public interface IActorView : IDamagedObject, IDestroyedObject
{
    public string Name { get; }
}