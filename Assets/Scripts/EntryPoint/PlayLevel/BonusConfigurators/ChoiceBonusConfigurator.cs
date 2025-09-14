using System;
using UnityEngine;

namespace PlayLevel
{
    public class ChoiceBonusConfigurator : BonusConfigurator
    {
        [Header("Choice bonus")]
        [SerializeField] private BonusChoiceMenu _bonusChoiceMenu;

        private void OnValidate()
        {
            if (_bonusChoiceMenu == null)
                throw new NullReferenceException(nameof(_bonusChoiceMenu));
        }

        public override void Configure()
        {
            ChoiceBonusActivator activator = new ChoiceBonusActivator(_bonusChoiceMenu);
            BonusPrefab.Initialize(activator);
        }
    }
}
