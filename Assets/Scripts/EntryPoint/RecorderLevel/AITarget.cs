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
        [SerializeField] private Color _targetColor = Color.yellow;
        [SerializeField] private Color _findRadiusColor = Color.green;
        [SerializeField, Min(0.1f)] private float _radusSphere = 0.5f;

        private Transform _transform;
        private Vector3 _startPosition;
        private float _correctionOffsetValue;
        private int _maxEnemiesCount;

        public Vector3 Position => _transform.position;

        public bool IsSelected { get; private set; }

        private void Awake()
        {
            _transform = transform;
            _startPosition = _startPoint.position;
            _correctionOffsetValue = 0.5f;
            _maxEnemiesCount = 9;

            IsSelected = false;
        }

        private void OnDrawGizmos()
        {
            if (_isDebugOn)
            {
                Gizmos.color = _targetColor;
                Gizmos.DrawSphere(transform.position, _radusSphere);
                Gizmos.DrawWireSphere(_startPosition, _findRadius);
            }
        }

        public void Select()
        {
            Vector3 enemyPosition = FindEnemyPosition();
            Vector3 direction = enemyPosition - _startPosition;
            float directionLength = direction.magnitude;
            Vector3 offsetDirection;

            if (direction.x > _correctionOffsetValue)
                offsetDirection = Vector3.right;
            else if (direction.x < -_correctionOffsetValue)
                offsetDirection = Vector3.left;
            else
                offsetDirection = Vector3.zero;

            offsetDirection = (direction.normalized + offsetDirection) * (directionLength * _offset);
            _transform.position = offsetDirection + enemyPosition;

            IsSelected = true;
        }

        public void ThrowOff()
        {
            IsSelected = false;
        }

        private Vector3 FindEnemyPosition()
        {
            Collider[] colliders = new Collider[_maxEnemiesCount];
            int count = Physics.OverlapSphereNonAlloc(_startPosition, _findRadius, colliders, _layerMask, QueryTriggerInteraction.Ignore);

            if (count != 0 && TryFindEnemy(out Collider enemy, colliders))
                return enemy.transform.position;
            else
                return Vector3.zero;
        }

        private bool TryFindEnemy(out Collider enemy, Collider[] colliders)
        {
            enemy = null;

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IEnemyView _))
                {
                    enemy = collider;

                    return true;
                }
            }

            return false;
        }
    }
}
