using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAdapter : MonoBehaviour
{
    [SerializeField] private Setting _horisontalSetting;

    [Header("Debug")]
    [SerializeField] private bool _isDebug = false;

    private Transform _transform;
    private Camera _camera;
    private Setting _portraitSetting;
    private bool _isPortraitOrientation;

    public float CameraHeight => _transform.position.y;

    private void Awake()
    {
        _transform = transform;
        _camera = GetComponent<Camera>();
        _isPortraitOrientation = true;
        _portraitSetting = new Setting();
        _portraitSetting.Position = _transform.position;
        _portraitSetting.Rotation = _transform.rotation.eulerAngles;
        _portraitSetting.FieldOfView = _camera.fieldOfView;
    }

    private void Start()
    {
        CheckCameraOrientation();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (_isDebug == false)
            CheckCameraOrientation();
        else
            ChangeSettingCamera(_horisontalSetting);
#else
        CheckCameraOrientation();
#endif
    }

    public Ray ScreenPointToRay(Vector2 position)
    {
        return _camera.ScreenPointToRay(position);
    }

    private void CheckCameraOrientation()
    {
        if (_isPortraitOrientation == Screen.width < Screen.height)
            return;

        _isPortraitOrientation = !_isPortraitOrientation;

        if (_isPortraitOrientation)
            ChangeSettingCamera(_portraitSetting);
        else
            ChangeSettingCamera(_horisontalSetting);
    }

    private void ChangeSettingCamera(Setting setting)
    {
        _transform.position = setting.Position;
        _transform.rotation = Quaternion.Euler(setting.Rotation);
        _camera.fieldOfView = setting.FieldOfView;
    }

    [System.Serializable]
    private struct Setting
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public float FieldOfView;
    }
}
