public interface IWallet
{
    public int CountCoins { get; }

    public void AddCoins(int countCoins);

    public bool TryPay(int countCoins);
}