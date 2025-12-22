using UnityEngine;

namespace RecorderLevel
{
    public class AITarget : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField, Min(5f)] private float _findRadius;
        [SerializeField, Range(0.01f, 1f)] private float _offset;

        [Header("Debug")]
        [SerializeField] private bool _isDebugOn = false;
        [SerializeField] private Color _color = Color.yellow;
        [SerializeField, Min(0.1f)] private float _radusSphere = 0.5f;

        private Transform _transform;
        private Vector3 _startPosition;
        private int _enemiesCount;

        public Vector3 Position => _transform.position;

        public bool IsSelected { get; private set; }

        private void Awake()
        {
            _transform = transform;
            _startPosition = _startPoint.position;
            _enemiesCount = 9;

            IsSelected = false;
        }

        private void OnDrawGizmos()
        {
            if (_isDebugOn)
            {
                Gizmos.color = _color;
                Gizmos.DrawSphere(transform.position, _radusSphere);
            }
        }

        public void Select()
        {
            Vector3 enemyPosition = FindEnemyPosition();
            Vector3 direction = enemyPosition - _startPosition;
            float directionLength = direction.magnitude;
            Vector3 offsetDirection;

            if (direction.y > 0f)
                offsetDirection = Vector3.right;
            else if (direction.y < 0f)
                offsetDirection = Vector3.left;
            else
                offsetDirection = Vector3.zero;

            Debug.Log($"Result vector == {direction.normalized * (directionLength * _offset)}");

            _transform.position = direction.normalized * (directionLength * _offset) + enemyPosition + offsetDirection;

            IsSelected = true;
        }

        public void ThrowOff()
        {
            IsSelected = false;
        }

        private Vector3 FindEnemyPosition()
        {
            Collider[] colliders = new Collider[_enemiesCount];
            int count = Physics.OverlapSphereNonAlloc(_startPosition, _findRadius, colliders, _layerMask, QueryTriggerInteraction.Ignore);

            Debug.Log($"count == {count}");

            foreach (var collider in colliders)
                if (collider.TryGetComponent(out IEnemyView _))
                    return collider.transform.position;

            return Vector3.zero;
        }
    }
}
