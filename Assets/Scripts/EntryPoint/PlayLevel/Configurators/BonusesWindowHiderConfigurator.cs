using System;
using UnityEngine;

namespace PlayLevel
{
    public class BonusesWindowHiderConfigurator : MonoBehaviour
    {
        [SerializeField] private BonusesWindowHider[] _bonusesWindowHiders;
        [SerializeField] private OpenWindowButton _openWindowButton;
        [SerializeField] private ReservedBonusesWindow _reservedBonusesWindow;

        private BonusesWindowHiderActivator _hiderActivator;

        private void OnValidate()
        {
            if (_openWindowButton == null)
                throw new ArgumentNullException(nameof(_openWindowButton));

            if (_reservedBonusesWindow == null)
                throw new ArgumentNullException(nameof(_reservedBonusesWindow));

            if (_bonusesWindowHiders == null || _bonusesWindowHiders.Length == 0)
                throw new InvalidOperationException(nameof(_bonusesWindowHiders));

            foreach (var bonusesWindowHider in _bonusesWindowHiders)
                if (bonusesWindowHider == null)
                    throw new NullReferenceException($"{_bonusesWindowHiders} contains null objects");
        }

        private void OnDisable()
        {
            _hiderActivator.Disable();
        }

        public void Configure(IShotAction shotAction)
        {
            if (shotAction == null)
                throw new ArgumentNullException(nameof(shotAction));

            foreach (var bonusesWindowHider in _bonusesWindowHiders)
                bonusesWindowHider.Initialize(_openWindowButton, _reservedBonusesWindow);

            _hiderActivator = new BonusesWindowHiderActivator(_openWindowButton, _reservedBonusesWindow, shotAction);
        }
    }
}
