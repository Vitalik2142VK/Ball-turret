using System;
using System.Linq;
using UnityEngine;

namespace PlayLevel
{
    public class ChoiceBonusConfigurator : BonusConfigurator
    {
        [Header("Choice bonus")]
        [SerializeField] private BonusChoiceMenu _bonusChoiceMenu;
        [SerializeField, SerializeIterface(typeof(IBonusCreator))] private GameObject[] _bonusCreators;

        private void OnValidate()
        {
            if (_bonusChoiceMenu == null)
                throw new NullReferenceException(nameof(_bonusChoiceMenu));

            if (_bonusCreators == null || _bonusCreators.Length == 0)
                throw new InvalidOperationException(nameof(_bonusCreators));
        }

        public override void Configure()
        {
            var bonusPrefabs = _bonusCreators.Select(bc => bc.GetComponent<IBonusCreator>().Create()).ToArray();
            BonusRandomizer bonusRandomizer = new BonusRandomizer(bonusPrefabs);
            _bonusChoiceMenu.Initialize(bonusRandomizer);

            ChoiceBonusActivator activator = new ChoiceBonusActivator(_bonusChoiceMenu);
            BonusPrefab.Initialize(activator);
        }
    }
}
