public interface IActorsController : IActorsPreparator
{
    public bool AreWavesOver { get; }

    public void Reboot();
}
