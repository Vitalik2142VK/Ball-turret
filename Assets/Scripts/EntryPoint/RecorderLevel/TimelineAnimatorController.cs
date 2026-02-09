using System.Collections;
using UnityEngine;

namespace RecorderLevel
{
    [RequireComponent(typeof(Animator))]
    public class TimelineAnimatorController : MonoBehaviour
    {
        private const string Hello = nameof(Hello);

        private Animator _animator;
        private int _hashHello;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _hashHello = Animator.StringToHash(Hello);
        }

        public void PlayHello(float timePlay)
        {
            WaitForSeconds wait = new WaitForSeconds(timePlay);

            _animator.SetBool(_hashHello, true);

            StartCoroutine(PlayHello(wait));
        }

        private IEnumerator PlayHello(YieldInstruction yieldInstruction)
        {
            yield return yieldInstruction;

            _animator.SetBool(_hashHello, false);
        }
    }
}
