using CannonTurret.UI.Animations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace CannonTurret.UI.MainMenu
{
    public class AuthWindow : MonoBehaviour
    {
        [SerializeField][SerializeIterface(typeof(IAnimatorUI))] private GameObject _authWindowAnimator;
        [SerializeField] private Button _confirmationButton;
        [SerializeField] private Button _cancelButton;

        private IAnimatorUI _animator;

        private void OnValidate()
        {
            if (_authWindowAnimator == null)
                throw new NullReferenceException(nameof(_authWindowAnimator));

            if (_confirmationButton == null)
                throw new NullReferenceException(nameof(_confirmationButton));

            if (_cancelButton == null)
                throw new NullReferenceException(nameof(_cancelButton));
        }

        private void Awake()
        {
            _animator = _authWindowAnimator.GetComponent<IAnimatorUI>();

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _confirmationButton.onClick.AddListener(OnAuthorize);
            _cancelButton.onClick.AddListener(OnClose);
        }

        private void OnDisable()
        {
            _confirmationButton.onClick.RemoveListener(OnAuthorize);
            _cancelButton.onClick.RemoveListener(OnClose);
        }

        public void Open()
        {
            gameObject.SetActive(true);
            _animator.Show();
        }

        private void OnAuthorize()
        {
            OnClose();

            YG2.OpenAuthDialog();
        }

        private void OnClose()
        {
            _animator.Hide();

            StartCoroutine(WaitClosure());
        }

        private IEnumerator WaitClosure()
        {
            yield return _animator.GetYieldAnimation();

            gameObject.SetActive(false);
        }
    }
}