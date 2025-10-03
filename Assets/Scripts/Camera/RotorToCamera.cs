using UnityEngine;

public class RotorToCamera : MonoBehaviour
{
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
        Vector3 rotationCamera = _cameraAdapter.Rotation;
        Vector3 newRotation = new Vector3(oldRotation.x, rotationCamera.y, oldRotation.z);

        _transform.rotation = Quaternion.Euler(newRotation);
    }
}