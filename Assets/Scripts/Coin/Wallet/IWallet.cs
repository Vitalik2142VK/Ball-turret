public interface IWallet
{
    public long CountCoins { get; }

    public void SetView(IWalletView walletView);

    public void AddCoins(int countCoins);

    public bool TryPay(int countCoins);
}