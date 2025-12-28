public interface IActorsController : IEnemyCounter, IActorsPreparator
{
    public bool AreWavesOver { get; }

    public void Reboot();
}
