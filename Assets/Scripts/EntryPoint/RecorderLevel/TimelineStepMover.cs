using DG.Tweening;
using System.Collections;
using UnityEngine;


namespace RecorderLevel
{
    [RequireComponent(typeof(TimelineEnemyAnimator))]
    public class TimelineStepMover : MonoBehaviour
    {
        private const float FinishDistance = 0.001f;

        [SerializeField] private Ease _ease;
        [SerializeField] private Vector3 _pointPosition;
        [SerializeField, Min(0.01f)] private float _stepSpeed = 1f;
        [SerializeField, Min(0.01f)] private float _stepLenght = 1f;

        private Transform _transform;
        private TimelineEnemyAnimator _animator;

        private bool IsMoving => Vector3.Distance(_pointPosition, _transform.position) > FinishDistance;

        private void Awake()
        {
            _transform = transform;
            _animator = GetComponent<TimelineEnemyAnimator>();
        }

        private void Start()
        {
            Move();
        }

        public void Move()
        {
            StartCoroutine(MoveRoutine());
        }

        private IEnumerator MoveRoutine()
        {
            while (IsMoving)
            {
                _animator.PlayMovement(true);

                Vector3 currentPosition = _transform.position;
                Vector3 direction = _pointPosition - currentPosition;
                float remainingDistance = direction.magnitude;
                float distanceStep = Mathf.Min(remainingDistance, _stepLenght);

                Vector3 nextPosition = direction.normalized * distanceStep + currentPosition;

                yield return _transform
                    .DOMove(nextPosition, _stepSpeed)
                    .SetEase(_ease)
                    .SetUpdate(true)
                    .Play()
                    .OnComplete(() => _animator.PlayMovement(false))
                    .WaitForCompletion();
            }
        }
    }
}
