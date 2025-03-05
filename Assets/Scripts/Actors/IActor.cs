public interface IActor : IMovableObject, IDestroyedObject
{
    public string Name { get; }

    public bool IsEnable { get; }
}
