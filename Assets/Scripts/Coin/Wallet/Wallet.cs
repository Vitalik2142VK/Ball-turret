using System;

public class Wallet : IWallet
{
    private IWalletView _walletView;

    public Wallet(long countCoinsPlayer)
    {
        if (countCoinsPlayer <= 0)
            throw new ArgumentOutOfRangeException(nameof(countCoinsPlayer));

        CountCoins = countCoinsPlayer;
    }

    public long CountCoins { get; private set; }

    public void SetView(IWalletView walletView)
    {
        _walletView = walletView ?? throw new ArgumentNullException(nameof(walletView));
        _walletView.UpdateValueCoins(CountCoins);
    }

    public void AddCoins(int countCoins)
    {
        if (countCoins <= 0)
            throw new ArgumentOutOfRangeException(nameof(countCoins));

        CountCoins += countCoins;

        _walletView.UpdateValueCoins(CountCoins);
    }

    public bool TryPay(int countCoins)
    {
        if (countCoins <= 0)
            throw new ArgumentOutOfRangeException(nameof(countCoins));

        if (countCoins <= CountCoins)
        {
            CountCoins -= countCoins;

            _walletView.UpdateValueCoins(CountCoins);

            return true;
        }

        return false;
    }
}
