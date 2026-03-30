using CannonTurret.CameraControl;
using UnityEngine;

namespace CannonTurret.UI.Animations
{
    [RequireComponent(typeof(Animator))]
    public class ClickerAnimator : MonoBehaviour
    {
        private const string IsVertival = nameof(IsVertival);

        private ICameraAdapter _cameraAdapter;
        private Animator _animator;
        private int _hashIsVertival;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            Camera camera = Camera.main;

            if (camera.TryGetComponent(out ICameraAdapter cameraAdapter) == false)
            {
                string message = $"The main camera does not contain the component: <{nameof(ICameraAdapter)}>";

                throw new System.InvalidOperationException(message);
            }

            _cameraAdapter = cameraAdapter;
            _hashIsVertival = Animator.StringToHash(IsVertival);
        }

        private void OnEnable()
        {
            _cameraAdapter.OrientationChanged += OnChangeAnimation;
        }

        private void Start()
        {
            OnChangeAnimation();
        }

        private void OnDisable()
        {
            _cameraAdapter.OrientationChanged -= OnChangeAnimation;
        }

        private void OnChangeAnimation()
        {
            _animator.SetBool(_hashIsVertival, _cameraAdapter.IsPortraitOrientation);
        }
    }
}