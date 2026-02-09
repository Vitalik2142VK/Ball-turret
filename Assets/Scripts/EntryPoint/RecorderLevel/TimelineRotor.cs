using UnityEngine;

namespace RecorderLevel
{
    public class TimelineRotor : MonoBehaviour
    {
        private const float MinDirectionSqrMagnitude = 0.0001f;

        [SerializeField] private Vector3 _viewingPoint;
        [SerializeField, Min(0.01f)] private float _speed = 1f;

        private Transform _transform;
        private bool _isRotationFinished;

        private void Awake()
        {
            _transform = transform;
            _isRotationFinished = false;
        }

        private void Update()
        {
            if (_isRotationFinished)
                return;

            Rotate();
        }

        public void SetViewingPoint(Vector3 point) => _viewingPoint = point;

        public void StartRotation()
        {
            _isRotationFinished = false;
        }

        public void Rotate()
        {
            Vector3 direction = _viewingPoint - _transform.position;

            if (direction.sqrMagnitude < MinDirectionSqrMagnitude)
            {
                _isRotationFinished = true;

                return;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, _speed);
        }
    }
}
