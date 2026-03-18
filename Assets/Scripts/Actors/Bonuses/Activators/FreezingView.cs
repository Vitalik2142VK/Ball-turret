using CannonTurret.AudioSystem;
using CannonTurret.Effects.Freezing;
using CannonTurret.UI.Animations;
using System;
using System.Collections;
using UnityEngine;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class FreezingView : MonoBehaviour, IBonusActicatorView
    {
        [SerializeField][SerializeIterface(typeof(IAnimatorUI))] private GameObject _imageFreeze;
        [SerializeField] private ActorsFreezerView _freezer;
        [SerializeField] private Sound _soundFreeze;

        private IAnimatorUI _animator;

        private void OnValidate()
        {
            if (_imageFreeze == null)
                throw new NullReferenceException(nameof(_imageFreeze));

            if (_freezer == null)
                throw new NullReferenceException(nameof(_freezer));

            if (_soundFreeze == null)
                throw new NullReferenceException(nameof(_soundFreeze));
        }

        private void Awake()
        {
            _animator = _imageFreeze.GetComponent<IAnimatorUI>();
            _imageFreeze.SetActive(false);
        }

        public void PlayActivation()
        {
            _freezer.Freeze();
            _imageFreeze.SetActive(true);
            _animator.Show();
            _soundFreeze.Play();

            StartCoroutine(WaitOpening());
        }

        private IEnumerator WaitOpening()
        {
            yield return _animator.GetYieldAnimation();

            _animator.Hide();

            StartCoroutine(WaitClosure());
        }

        private IEnumerator WaitClosure()
        {
            yield return _animator.GetYieldAnimation();

            _imageFreeze.SetActive(false);
        }
    }
}