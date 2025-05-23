using System;

public class PriceEnlarger : IPriceEnlarger
{
    public const float MinMagnificationFactor = 1.1f;

    private float _magnificationFactor;
    private int _initialPrice;

    public PriceEnlarger(int initialPrice, float magnificationFactor = MinMagnificationFactor)
    {
        if (initialPrice < 0)
            throw new ArgumentNullException(nameof(initialPrice));

        if (magnificationFactor < MinMagnificationFactor)
            throw new ArgumentNullException(nameof(magnificationFactor));

        _initialPrice = initialPrice;
        _magnificationFactor = magnificationFactor;

        Price = _initialPrice;
    }

    public int Price { get; private set; }

    public void IncreaseByLevel(int levelImprovement)
    {
        if (levelImprovement < 0)
            throw new ArgumentNullException(nameof(levelImprovement));

        float pow = MathTool.Pow(_magnificationFactor, levelImprovement);

        Price = (int)Math.Round(_initialPrice * pow);
    }
}
