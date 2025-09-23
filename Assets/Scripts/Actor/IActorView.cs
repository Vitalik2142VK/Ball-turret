public interface IActorView : IDestroyedObject
{
    public string Name { get; }

    public IActor Actor { get; }
}