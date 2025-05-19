public interface IActorsController
{
    public bool AreNoEnemies { get; }

    public bool AreWavesOver { get; }

    public void Reboot();
}
