using System;
using System.Collections.Generic;

public class BonusRandomizer : IBonusRandomizer
{
    private List<IBonus> _prefabs;
    private Random _random;

    public BonusRandomizer(IEnumerable<IBonus> prefabs)
    {
        if (prefabs == null)
            throw new ArgumentNullException(nameof(prefabs));

        _prefabs = new List<IBonus>(prefabs);

        if (_prefabs.Count == 0)
            throw new ArgumentOutOfRangeException(nameof(prefabs));

        _random = new Random();
    }

    public IEnumerable<IBonus> GetBonuses(int countBonuses)
    {
        if (countBonuses < _prefabs.Count)
            throw new ArgumentOutOfRangeException(nameof(countBonuses));

        if (countBonuses == _prefabs.Count)
            return _prefabs.ToArray();

        IBonus[] randomBonuses = new IBonus[countBonuses];

        for (int i = 0; i < randomBonuses.Length; i++)
        {
            int randomIndex = _random.Next(0, _prefabs.Count);
            randomBonuses[i] = _prefabs[randomIndex];
            _prefabs.RemoveAt(randomIndex);
        }

        _prefabs.AddRange(randomBonuses);

        return randomBonuses;
    }
}