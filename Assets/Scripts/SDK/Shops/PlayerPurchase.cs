using YG;

public class PlayerPurchase : IPlayerPurchase
{
    private OneTimePurchase _purchase;

    public PlayerPurchase(OneTimePurchase purchase)
    {
        _purchase = purchase;
    }

    public string Id => _purchase.Id;
    public bool IsPurchased => _purchase.IsPurchased;

    public void Update()
    {
        _purchase = YG2.saves.GetOneTimePurchase(_purchase.Id);
    }
}
