using System;

public class PriceEnlarger : IPriceEnlarger
{
    private const float MinMagnificationFactor = 1.1f;
    private const float MinLowImprovementCoefficient = 0.01f;

    private float _magnificationFactor;
    private float _lowImprovementCoefficient;
    private int _initialPrice;
    private int _maxLevelImprovement;

    public PriceEnlarger(int initialPrice, int maxLevelImprovement, float magnificationFactor = MinMagnificationFactor, float lowImprovementCoefficient = MinLowImprovementCoefficient)
    {
        if (initialPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(initialPrice));

        if (maxLevelImprovement < 0)
            throw new ArgumentOutOfRangeException(nameof(maxLevelImprovement));

        if (magnificationFactor < MinMagnificationFactor)
            throw new ArgumentOutOfRangeException(nameof(magnificationFactor));

        if (lowImprovementCoefficient < MinLowImprovementCoefficient)
            throw new ArgumentOutOfRangeException(nameof(lowImprovementCoefficient));

        _initialPrice = initialPrice;
        _maxLevelImprovement = maxLevelImprovement;
        _magnificationFactor = magnificationFactor;
        _lowImprovementCoefficient = lowImprovementCoefficient;

        Price = _initialPrice;
    }

    public int Price { get; private set; }

    public void IncreaseByLevel(int levelImprovement)
    {
        if (levelImprovement < 0)
            throw new ArgumentNullException(nameof(levelImprovement));

        float lowImprovementCoefficient = (float)Math.Exp(levelImprovement * _lowImprovementCoefficient);

        if (levelImprovement > _maxLevelImprovement)
            levelImprovement = _maxLevelImprovement;

        float improvementCoefficient = MathTool.Pow(_magnificationFactor, levelImprovement);

        Price = (int)Math.Round(_initialPrice * improvementCoefficient * lowImprovementCoefficient);
    }
}
