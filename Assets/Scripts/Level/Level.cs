using System;

public class Level : ILevel
{
    public Level(ILevelActorsPlanner actorsPlanner, float actorsHealthCoefficient, int countCoinsWin, int countCoinsDefeat, int index)
    {
        if (actorsHealthCoefficient < 0f)
            throw new ArgumentOutOfRangeException("The coefficient cannot be less than 0");

        if (countCoinsWin <= 0 || countCoinsDefeat <= 0)
            throw new ArgumentOutOfRangeException("The number of coins cannot be equal to or less than 0");

        if (countCoinsWin < countCoinsDefeat)
            throw new ArgumentOutOfRangeException("The number of coins for a victory cannot be less than for a defeat");

        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        ActorsPlanner = actorsPlanner ?? throw new ArgumentNullException(nameof(actorsPlanner));

        ActorsHealthCoefficient = actorsHealthCoefficient;
        CountCoinsWin = countCoinsWin;
        CountCoinsDefeat = countCoinsDefeat;
        Index = index;
    }

    public ILevelActorsPlanner ActorsPlanner { get; private set; }
    public float ActorsHealthCoefficient { get; private set; }
    public int CountCoinsWin { get; private set; }
    public int CountCoinsDefeat { get; private set; }
    public int Index { get; private set; }
}
