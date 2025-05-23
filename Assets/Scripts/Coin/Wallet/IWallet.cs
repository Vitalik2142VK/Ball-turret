public interface IWallet
{
    public int CountCoins { get; }

    public void SetView(IWalletView walletView);

    public void AddCoins(int countCoins);

    public bool TryPay(int countCoins);
}