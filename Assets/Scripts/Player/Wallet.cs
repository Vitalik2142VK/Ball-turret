using System;

public class Wallet : IWallet
{
    public Wallet(int countCoinsPlayer)
    {
        if (countCoinsPlayer <= 0)
            throw new ArgumentOutOfRangeException(nameof(countCoinsPlayer));

        CountCoins = countCoinsPlayer;
    }

    public int CountCoins { get; private set; }

    public void AddCoins(int countCoins)
    {
        if (countCoins <= 0)
            throw new ArgumentOutOfRangeException(nameof(countCoins));

        CountCoins += countCoins;
    }

    public bool TryPay(int countCoins)
    {
        if (countCoins <= 0)
            throw new ArgumentOutOfRangeException(nameof(countCoins));

        if (countCoins <= CountCoins)
        {
            CountCoins -= countCoins;

            return true;
        }

        return false;
    }
}