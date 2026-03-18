using UnityEngine;

namespace CannonTurret.CameraControl
{
    public class RotorToCamera : MonoBehaviour
    {
        [SerializeField] private float _angle = 0f;

        private ICameraAdapter _cameraAdapter;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            Camera camera = Camera.main;

            if (camera.TryGetComponent(out ICameraAdapter cameraAdapter) == false)
                throw new System.InvalidOperationException($"The main camera does not contain the component: <{nameof(ICameraAdapter)}>");

            _cameraAdapter = cameraAdapter;
        }

        private void OnEnable()
        {
            _cameraAdapter.OrientationChanged += OnRotate;
        }


        private void Start()
        {
            OnRotate();
        }

        private void OnDisable()
        {
            _cameraAdapter.OrientationChanged -= OnRotate;
        }

        private void OnRotate()
        {
            Vector3 oldRotation = _transform.rotation.eulerAngles;
            float rotationY = _cameraAdapter.Rotation.y + _angle;
            Vector3 newRotation = new Vector3(oldRotation.x, rotationY, oldRotation.z);

            _transform.rotation = Quaternion.Euler(newRotation);
        }
    }
}