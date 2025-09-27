using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAdapter : MonoBehaviour, ICameraAdapter
{
    [SerializeField] private Setting _horisontalSetting;

    [Header("Debug")]
    [SerializeField] private bool _isDebug = false;

    private Transform _transform;
    private Camera _camera;
    private Setting _portraitSetting;

    public event System.Action OrientationChanged;

    public float CameraHeight => _transform.position.y;
    public Vector3 Rotation => _portraitSetting.Rotation;

    public bool IsPortraitOrientation { get; private set; }

    private void Awake()
    {
        _transform = transform;
        _camera = GetComponent<Camera>();
        IsPortraitOrientation = true;
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
        if (_isDebug)
            ChangeSettingCamera(_horisontalSetting);
        else
            CheckCameraOrientation();
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
        if (IsPortraitOrientation == Screen.width < Screen.height)
            return;

        IsPortraitOrientation = !IsPortraitOrientation;

        if (IsPortraitOrientation)
            ChangeSettingCamera(_portraitSetting);
        else
            ChangeSettingCamera(_horisontalSetting);

        OrientationChanged?.Invoke();
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
