using UnityEngine;

namespace RecorderLevel
{
    [RequireComponent(typeof(Animator))]
    public class TimelineAnimatorController : MonoBehaviour
    {
        private const string Hello = nameof(Hello);
        private const string Collect = nameof(Collect);
        private const string Walk = nameof(Walk);
        private const string GetUp = nameof(GetUp);
        private const string PanicRun = nameof(PanicRun);
        private const string Run = nameof(Run);

        private Animator _animator;
        private int _hashHello;
        private int _hashCollect;
        private int _hashWalk;
        private int _hashGetUp;
        private int _hashPanicRun;
        private int _hashRun;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _hashHello = Animator.StringToHash(Hello);
            _hashCollect = Animator.StringToHash(Collect);
            _hashWalk = Animator.StringToHash(Walk);
            _hashGetUp = Animator.StringToHash(GetUp);
            _hashPanicRun = Animator.StringToHash(PanicRun);
            _hashRun = Animator.StringToHash(Run);
        }

        public void SetSpeedAnimator(float speed) => _animator.speed = speed;

        public void PlayWalk(bool isPlay) => _animator.SetBool(_hashWalk, isPlay);

        public void PlayPanicRun(bool isPlay) => _animator.SetBool(_hashPanicRun, isPlay);

        public void PlayRunning(bool isPlay) => _animator.SetBool(_hashRun, isPlay);

        public void PlayCollect() => _animator.SetTrigger(_hashCollect);

        public void PlayHello() => _animator.SetTrigger(_hashHello);

        public void PlayGetUp() => _animator.SetTrigger(_hashGetUp);
    }
}
