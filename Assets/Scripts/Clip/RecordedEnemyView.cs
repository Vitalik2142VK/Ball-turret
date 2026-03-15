using CannonTurret.Actors;
using CannonTurret.Actors.Enemies;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CannonTurret.Clip
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class RecordedEnemyView : MonoBehaviour
    {
        [SerializeField] private ActorParticleController _particleController;
        [SerializeField] private SkinnedMeshRenderer _meshRenderer;
        [SerializeField] private ActorAudioController _audioController;
        [SerializeField] private Image _shadow;

        private IEnemyAnimator _enemyAnimator;
        private bool _isActive;

        private void OnValidate()
        {
            if (_particleController == null)
                throw new NullReferenceException(nameof(_particleController));

            if (_meshRenderer == null)
                throw new NullReferenceException(nameof(_meshRenderer));

            if (_audioController == null)
                throw new NullReferenceException(nameof(_audioController));

            if (_shadow == null)
                throw new NullReferenceException(nameof(_shadow));
        }

        private void Awake()
        {
            _enemyAnimator = GetComponent<IEnemyAnimator>();
        }

        private void OnEnable()
        {
            SetEnable(true);
        }

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void PlayDead()
        {
            if (_isActive)
                StartCoroutine(StartDeadProcess());
        }

        private void SetEnable(bool isEnable)
        {
            _isActive = isEnable;
            _meshRenderer.enabled = isEnable;
            _shadow.gameObject.SetActive(isEnable);
        }

        private IEnumerator StartDeadProcess()
        {
            _isActive = false;
            _enemyAnimator.PlayDead();

            yield return new WaitForSeconds(_enemyAnimator.TimeCompletionDeath);

            SetEnable(false);

            _audioController.PlayDead();
            _particleController.PlayDead();
        }
    }
}