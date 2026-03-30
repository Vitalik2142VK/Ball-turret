using System;
using UnityEngine;

namespace CannonTurret.UI.PlayerScene
{
    public class BonusesWindowHider : MonoBehaviour
    {
        [SerializeField] private bool _isShowIfEnbale = true;

        private IOpenWindowButton _openWindowButton;
        private IReservedBonusesWindow _reservedBonusesWindow;

        private bool _wasButtonActive;

        private void OnEnable()
        {
            if (_isShowIfEnbale)
                Show();
            else
                Hide();
        }

        private void OnDisable()
        {
            if (_isShowIfEnbale)
                Hide();
            else
                Show();
        }

        public void Initialize(IOpenWindowButton openWindowButton, IReservedBonusesWindow reservedBonusesWindow)
        {
            if (openWindowButton == null)
                throw new ArgumentNullException(nameof(openWindowButton));

            if (reservedBonusesWindow == null)
                throw new ArgumentNullException(nameof(reservedBonusesWindow));

            _openWindowButton = openWindowButton;
            _reservedBonusesWindow = reservedBonusesWindow;
            _wasButtonActive = false;
        }

        private void Show()
        {
            if (_openWindowButton == null || _reservedBonusesWindow == null)
                return;

            if (_wasButtonActive)
                _openWindowButton.Show();
        }

        private void Hide()
        {
            if (_openWindowButton == null || _reservedBonusesWindow == null)
                return;

            if (_openWindowButton.IsActive || _reservedBonusesWindow.IsActive)
                _wasButtonActive = true;
            else
                _wasButtonActive = false;

            if (_openWindowButton.IsActive)
                _openWindowButton.Hide();

            if (_reservedBonusesWindow.IsActive)
                _reservedBonusesWindow.Hide();
        }
    }
}