using System;
using UnityEngine;

public class AdapterPlaneToCamera : MonoBehaviour
{
    [SerializeField] private GameObject _verticalPlane;
    [SerializeField] private GameObject _horisontalPlane;

    private ICameraAdapter _cameraAdapter;

    private void OnValidate()
    {
        if (_verticalPlane == null)
            throw new NullReferenceException(nameof(_verticalPlane));

        if (_horisontalPlane == null)
            throw new NullReferenceException(nameof(_horisontalPlane));
    }

    private void Awake()
    {
        Camera camera = Camera.main;

        if (camera.TryGetComponent(out ICameraAdapter cameraAdapter) == false)
            throw new InvalidOperationException($"The main camera does not contain the component: <{nameof(ICameraAdapter)}>");

        _cameraAdapter = cameraAdapter;
        _verticalPlane.SetActive(true);
        _horisontalPlane.SetActive(false);
    }

    private void OnEnable()
    {
        _cameraAdapter.OrientationChanged += OnChangePlane;
    }


    private void Start()
    {
        OnChangePlane();
    }

    private void OnDisable()
    {
        _cameraAdapter.OrientationChanged -= OnChangePlane;
    }

    private void OnChangePlane()
    {
        bool isPortraitOrientation = _cameraAdapter.IsPortraitOrientation;

        _verticalPlane.SetActive(isPortraitOrientation);
        _horisontalPlane.SetActive(isPortraitOrientation == false);
    }
}
