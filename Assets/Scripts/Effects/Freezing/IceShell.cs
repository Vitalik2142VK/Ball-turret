using System;
using System.Collections;
using UnityEngine;

namespace CannonTurret.Effects.Freezing
{
    [RequireComponent(typeof(ScaleAnimator), typeof(MeshRenderer))]
    public class IceShell : MonoBehaviour, IIceShell
    {
        [SerializeField] private Vector3 _positionOffset = Vector3.zero;

        private IIceShellPool _pool;
        private Transform _transform;
        private MeshRenderer _mesh;
        private ScaleAnimator _animator;

        public event Action Disabled;

        private void Awake()
        {
            _transform = transform;
            _mesh = GetComponent<MeshRenderer>();
            _animator = GetComponent<ScaleAnimator>();
            _animator.Initicalize();
        }

        private void OnEnable()
        {
            _mesh.enabled = false;
        }

        public void Initialize(IIceShellPool pool)
        {
            _pool = pool ?? throw new NullReferenceException(nameof(pool));
        }

        public void SetScale(Vector3 scale)
        {
            if (scale == Vector3.zero)
                throw new ArgumentOutOfRangeException(nameof(scale));

            _transform.localScale = scale;
            _animator.UpdateScale();
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position + _positionOffset;
        }

        public void Enable()
        {
            _transform.rotation = UnityEngine.Random.rotation;
            _animator.Show();
            _mesh.enabled = true;
        }

        public void Disable()
        {
            _animator.Hide();

            Disabled?.Invoke();

            StartCoroutine(WaitEndAnimation());
        }

        private IEnumerator WaitEndAnimation()
        {
            yield return _animator.GetYieldAnimation();

            _pool.Put(this);
        }
    }
}