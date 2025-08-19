public interface IPurchasesStorage
{
    public bool TryGetPurchase(out IPlayerPurchase purchase, string id);
}
