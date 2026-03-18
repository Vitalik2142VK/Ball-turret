using System;
using System.Collections.Generic;

namespace CannonTurret.Actors.Bonuses
{
    public class BonusRandomizer : IBonusRandomizer
    {
        private readonly List<IBonus> Prefabs;
        private readonly Random Random;

        public BonusRandomizer(IEnumerable<IBonus> prefabs)
        {
            if (prefabs == null)
                throw new ArgumentNullException(nameof(prefabs));

            Prefabs = new List<IBonus>(prefabs);

            if (Prefabs.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(prefabs));

            Random = new Random();
        }

        public IEnumerable<IBonus> GetBonuses(int countBonuses)
        {
            if (countBonuses > Prefabs.Count)
                throw new ArgumentOutOfRangeException(nameof(countBonuses));

            if (countBonuses == Prefabs.Count)
                return Prefabs.ToArray();

            IBonus[] randomBonuses = new IBonus[countBonuses];

            for (int i = 0; i < randomBonuses.Length; i++)
            {
                int randomIndex = Random.Next(0, Prefabs.Count);
                randomBonuses[i] = Prefabs[randomIndex];
                Prefabs.RemoveAt(randomIndex);
            }

            Prefabs.AddRange(randomBonuses);

            return randomBonuses;
        }
    }
}