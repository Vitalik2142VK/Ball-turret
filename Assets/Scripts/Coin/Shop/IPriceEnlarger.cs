public interface IPriceEnlarger
{
    public int Price { get; }

    public void IncreaseByLevel(int levelImprovement);
}