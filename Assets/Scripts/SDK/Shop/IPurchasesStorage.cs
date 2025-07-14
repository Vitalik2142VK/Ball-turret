public interface IPurchasesStorage
{
    public bool IsContainsId(string id);

    public IPurchase GetPurchase(string id);
}
