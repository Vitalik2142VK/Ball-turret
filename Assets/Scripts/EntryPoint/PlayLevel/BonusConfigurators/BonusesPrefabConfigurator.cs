using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayLevel
{
    public class BonusesPrefabConfigurator : MonoBehaviour
    {
        [SerializeField] private BonusConfigurator[] _bonusConfigurators;
        [SerializeField] private BonusFactory _bonusFactory;

        private void OnValidate()
        {
            if (_bonusConfigurators == null || _bonusConfigurators.Length == 0)
                throw new InvalidOperationException(nameof(_bonusConfigurators));

            foreach (var bonusConfigurator in _bonusConfigurators)
                if (bonusConfigurator == null)
                    throw new NullReferenceException($"{_bonusConfigurators} contains null objects");

            if (_bonusFactory == null)
                throw new NullReferenceException(nameof(_bonusFactory));
        }

        public void Configure()
        {
            foreach (var bonusConfigurator in _bonusConfigurators)
                bonusConfigurator.Configure();

            InitializeBonusFactory();
        }

        private void InitializeBonusFactory()
        {
            var bonusCreators = _bonusConfigurators.Select(bc => bc.Creator).ToArray();
            List<IViewableBonusCreator> collisionBonusCreators = new List<IViewableBonusCreator>();

            foreach (var creator in bonusCreators)
                if (creator is IViewableBonusCreator viewableBonusCreator)
                    collisionBonusCreators.Add(viewableBonusCreator);

            _bonusFactory.Initialize(collisionBonusCreators);
        }
    }
}
