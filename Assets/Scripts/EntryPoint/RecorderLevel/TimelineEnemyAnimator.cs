using UnityEngine;

namespace RecorderLevel
{
    [RequireComponent(typeof(Animator))]
    public class TimelineEnemyAnimator : MonoBehaviour
    {
        private const string Run = nameof(Run);

        private Animator _animator;
        private int _hashRun;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _hashRun = Animator.StringToHash(Run);
        }

        public void PlayMovement(bool isPlay) => _animator.SetBool(_hashRun, isPlay);
    }
}
