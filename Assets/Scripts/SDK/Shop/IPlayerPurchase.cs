public interface IPlayerPurchase
{
    public string Id { get; }
    public bool IsPurchased { get; }

    public void Update();
}
