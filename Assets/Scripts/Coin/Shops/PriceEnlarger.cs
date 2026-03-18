using CannonTurret.Utils;
using System;

namespace CannonTurret.Coin.Shops
{
    public class PriceEnlarger : IPriceEnlarger
    {
        private const float MinMagnificationFactor = 1.1f;
        private const float MinLowImprovementCoefficient = 0.01f;

        private readonly float MagnificationFactor;
        private readonly float LowImprovementCoefficient;
        private readonly int InitialPrice;
        private readonly int MaxLevelImprovement;

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

            InitialPrice = initialPrice;
            MaxLevelImprovement = maxLevelImprovement;
            MagnificationFactor = magnificationFactor;
            LowImprovementCoefficient = lowImprovementCoefficient;

            Price = InitialPrice;
        }

        public int Price { get; private set; }

        public void IncreaseByLevel(int levelImprovement)
        {
            if (levelImprovement < 0)
                throw new ArgumentNullException(nameof(levelImprovement));

            float lowImprovementCoefficient = (float)Math.Exp(levelImprovement * LowImprovementCoefficient);

            if (levelImprovement > MaxLevelImprovement)
                levelImprovement = MaxLevelImprovement;

            float improvementCoefficient = MathTool.Pow(MagnificationFactor, levelImprovement);

            Price = (int)Math.Round(InitialPrice * improvementCoefficient * lowImprovementCoefficient);
        }
    }
}