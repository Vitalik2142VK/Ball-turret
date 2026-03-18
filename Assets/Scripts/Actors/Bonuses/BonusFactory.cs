using System;
using System.Collections.Generic;

namespace CannonTurret.Actors.Bonuses
{
    public class BonusFactory
    {
        private readonly Dictionary<string, IBonusCreator> Creators;

        public BonusFactory(IEnumerable<IBonusCreator> bonusCreators)
        {
            if (bonusCreators == null)
                throw new ArgumentNullException(nameof(bonusCreators));

            Creators = CreateDictionaryPrefabs(bonusCreators);
        }

        public IBonus Create(string nameTypeActor)
        {
            if (IsCanCreate(nameTypeActor) == false)
                throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

            return Creators[nameTypeActor].Create();
        }

        private bool IsCanCreate(string nameTypeActor)
        {
            if (nameTypeActor == null || nameTypeActor.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

            return Creators.ContainsKey(nameTypeActor);
        }

        private Dictionary<string, IBonusCreator> CreateDictionaryPrefabs(IEnumerable<IBonusCreator> bonusPrefabs)
        {
            Dictionary<string, IBonusCreator> creators = new Dictionary<string, IBonusCreator>();

            foreach (var creator in bonusPrefabs)
                creators.Add(creator.Name, creator);

            if (creators.Count == 0)
                throw new InvalidOperationException($"{nameof(bonusPrefabs)} should not be empty");

            return creators;
        }
    }
}