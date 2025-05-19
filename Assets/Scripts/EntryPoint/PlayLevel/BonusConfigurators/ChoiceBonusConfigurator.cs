using System;
using UnityEngine;

namespace PlayLevel
{
    public class ChoiceBonusConfigurator : BonusConfigurator
    {
        [Header("Choice bonus")]
        [SerializeField] private ChoiceBonusMenu _choiceBonusMenu;

        private void OnValidate()
        {
            if (_choiceBonusMenu == null)
                throw new NullReferenceException(nameof(_choiceBonusMenu));
        }

        public override void Configure()
        {
            ChoiceBonusActivator activator = new ChoiceBonusActivator(_choiceBonusMenu);

            BonusPrefab.Initialize(activator);
        }
    }
}
