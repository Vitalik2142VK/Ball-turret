public interface IHealth : IDamagedObject
{
    public bool IsAlive { get; }

    public void Restore();
}
