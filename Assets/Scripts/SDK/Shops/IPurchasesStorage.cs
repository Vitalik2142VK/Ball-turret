namespace CannonTurret.SDK.Shops
{
    public interface IPurchasesStorage
    {
        public bool TryGetPurchase(out IPlayerPurchase purchase, string id);
    }
}