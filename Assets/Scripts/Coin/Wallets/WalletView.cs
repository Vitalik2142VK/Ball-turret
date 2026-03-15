using System;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour, IWalletView
{
    [SerializeField] private TextMeshProUGUI _countCoins;

    private void OnValidate()
    {
        if (_countCoins == null)
            throw new NullReferenceException(nameof(_countCoins));
    }

    public void UpdateValueCoins(long coutnCoins)
    {
        if (coutnCoins < 0)
            throw new ArgumentOutOfRangeException(nameof(_countCoins));

        _countCoins.text = coutnCoins.ToString();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
