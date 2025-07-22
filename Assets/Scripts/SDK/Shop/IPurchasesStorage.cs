public interface IPurchasesStorage
{
    public bool HasPurchseId(string id);

    public IPurchase GetPurchase(string id);

    public bool TryGetPurchase(out IPurchase purchase, string id);
}
