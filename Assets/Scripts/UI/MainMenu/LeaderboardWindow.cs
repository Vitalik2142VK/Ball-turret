using CannonTurret.UI.Animations;
using System;
using System.Collections;
using UnityEngine;

namespace CannonTurret.UI.MainMenu
{
    [RequireComponent(typeof(HiderUI), typeof(ShiftAnimatorUI))]
    public class LeaderboardWindow : MonoBehaviour
    {
        private IWindow _previousWindow;
        private IAnimatorUI _animator;
        private HiderUI _hiderUI;

        private void Awake()
        {
            _hiderUI = GetComponent<HiderUI>();
            _animator = GetComponent<ShiftAnimatorUI>();

            gameObject.SetActive(false);
        }

        public void OnClose()
        {
            _hiderUI.Show();
            _animator.Hide();

            StartCoroutine(WaitClosure());
        }

        public void Open(IWindow previousWindow)
        {
            _previousWindow = previousWindow ?? throw new ArgumentNullException(nameof(previousWindow));

            gameObject.SetActive(true);
            _hiderUI.Hide();
            _animator.Show();
        }

        private IEnumerator WaitClosure()
        {
            yield return _animator.GetYieldAnimation();

            gameObject.SetActive(false);
            _previousWindow.Enable();
        }
    }
}