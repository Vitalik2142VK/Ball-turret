using CannonTurret.AudioSystem;
using System;
using System.Collections;
using UnityEngine;

namespace CannonTurret.Effects
{
    public class RocketView : MonoBehaviour, IRocketView
    {
        private const string Fly = nameof(Fly);

        [SerializeField][SerializeIterface(typeof(ISound))] private GameObject _flySoundGameObject;
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _flyRocketParticle;
        [SerializeField] private MeshRenderer _meshRenderer;

        private ISound _flySound;
        private WaitForSeconds _waitFly;
        private int _hashFly;

        public event Action RocketFinished;

        private void OnValidate()
        {
            if (_flySoundGameObject == null)
                throw new NullReferenceException(nameof(_flySoundGameObject));

            if (_animator == null)
                throw new NullReferenceException(nameof(_animator));

            if (_flyRocketParticle == null)
                throw new NullReferenceException(nameof(_flyRocketParticle));

            if (_meshRenderer == null)
                throw new NullReferenceException(nameof(_meshRenderer));
        }

        private void Awake()
        {
            _flySound = _flySoundGameObject.GetComponent<ISound>();

            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            AnimationClip flyClip = Array.Find(clips, c => c.name == Fly);
            float flyRocketTime = flyClip.length / _animator.speed;

            _waitFly = new WaitForSeconds(flyRocketTime);
            _hashFly = Animator.StringToHash(Fly);

            _meshRenderer.enabled = false;
            _flyRocketParticle.Stop();
        }

        public void Play()
        {
            _meshRenderer.enabled = true;
            _animator.SetTrigger(_hashFly);
            _flyRocketParticle.Play();
            _flySound.Play();

            StartCoroutine(WaitFly());
        }

        private IEnumerator WaitFly()
        {
            yield return _waitFly;

            _meshRenderer.enabled = false;
            _flyRocketParticle.Stop();
            _flySound.Stop();

            RocketFinished?.Invoke();
        }
    }
}