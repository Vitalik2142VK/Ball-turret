public interface IGamePayTransaction
{
    public int Price { get; }
    public bool IsLocked { get; }

    public bool TrySpend(IWallet wallet);
}
