using UnityEngine;
using UnityEngine.UIElements;
using YG;

[RequireComponent(typeof(Camera))]
public class CameraAdapter : MonoBehaviour
{
    [SerializeField] private Setting _horisontal;

    [Header("Setting for Desktop")]
    [SerializeField] private Vector3 _position;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _fieldOfView;

    private Transform _transform;
    private Camera _camera;
    private Setting _vertical;

    public float CameraHeight => _transform.position.y;

    private void Awake()
    {
        _transform = transform;
        _camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        YG2.onGetSDKData += OnCheckDevice;
    }

    private void Start()
    {
        if (YG2.isSDKEnabled)
            OnCheckDevice();
    }

    private void Update()
    {
#if UNITY_EDITOR
        //ChangeSettingCamera();
#endif
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= OnCheckDevice;
    }

    public Ray ScreenPointToRay(Vector2 position)
    {
        return _camera.ScreenPointToRay(position);
    } 
    private void OnCheckDevice()
    {
        var device = YG2.envir.device;

        if (device == YG2.Device.Desktop)
            ChangeSettingCamera();
    }

    private void ChangeSettingCamera()
    {
        _transform.position = _position;
        _transform.rotation = Quaternion.Euler(_rotation);
        _camera.fieldOfView = _fieldOfView;
    }

    [System.Serializable]
    private struct Setting
    {
        public Vector3 _position;
        public Vector3 _rotation;
        public float _fieldOfView;
    }
}
