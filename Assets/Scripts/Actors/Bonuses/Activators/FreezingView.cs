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
        [SerializeField][SerializeIterface(typeof(IActorsFreezerView))] private GameObject _freezerGameObject;
        [SerializeField] private Sound _soundFreeze;

        private IAnimatorUI _animator;
        private IActorsFreezerView _freezer;

        private void OnValidate()
        {
            if (_imageFreeze == null)
                throw new NullReferenceException(nameof(_imageFreeze));

            if (_freezerGameObject == null)
                throw new NullReferenceException(nameof(_freezerGameObject));

            if (_soundFreeze == null)
                throw new NullReferenceException(nameof(_soundFreeze));
        }

        private void Awake()
        {
            _animator = _imageFreeze.GetComponent<IAnimatorUI>();
            _freezer = _freezerGameObject.GetComponent<IActorsFreezerView>();
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