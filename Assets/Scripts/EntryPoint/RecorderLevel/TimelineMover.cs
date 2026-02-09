using UnityEngine;

namespace RecorderLevel
{
    [RequireComponent (typeof (TimelineRotor))]
    public class TimelineMover : MonoBehaviour
    {
        [SerializeField] private Vector3 _pointPosition;
        [SerializeField, Min(0.01f)] private float _speed = 1f;

        private Transform _transform;
        private TimelineRotor _rotor;
        private float _distanceFinished;

        private bool IsFinished => Vector3.Distance(_pointPosition, _transform.position) < _distanceFinished;

        private void Awake()
        {
            _rotor = GetComponent<TimelineRotor>();

            _transform = transform;
            _distanceFinished = 0.01f;
        }

        private void Update()
        {
            if (IsFinished)
                return;

            Move();
        }

        private void Move()
        {
            Vector3 currentPosition = _transform.position;

            _transform.position = Vector3.MoveTowards(currentPosition, _pointPosition, _speed * Time.deltaTime);
            _rotor.SetViewingPoint(_pointPosition);
            _rotor.Rotate();
        }
    }
}
