using System;
using UnityEngine;

namespace PlayLevel
{
    public class BonusesWindowHiderConfigurator : MonoBehaviour
    {
        [SerializeField] private AdvancedOpenWindowButton _openWindowButton;
        [SerializeField] private ReservedBonusesWindow _reservedBonusesWindow;
        [SerializeField] private BonusesWindowHider _bonusesWindowHider;

        private void OnValidate()
        {
            if (_openWindowButton == null)
                throw new ArgumentNullException(nameof(_openWindowButton));

            if (_reservedBonusesWindow == null)
                throw new ArgumentNullException(nameof(_reservedBonusesWindow));

            if (_bonusesWindowHider == null)
                throw new ArgumentNullException(nameof(_bonusesWindowHider));
        }

        public void Configure()
        {
            _bonusesWindowHider.Initialize(_openWindowButton, _reservedBonusesWindow);
        }
    }
}
