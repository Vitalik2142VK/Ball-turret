using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAdapter : MonoBehaviour
{
    [SerializeField] private Setting _horisontalSetting;

    private Transform _transform;
    private Camera _camera;
    private Setting _portraitSetting;
    private bool _isPortraitOrientation;

    public float CameraHeight => _transform.position.y;

    private void Awake()
    {
        _transform = transform;
        _camera = GetComponent<Camera>();
        _isPortraitOrientation = Screen.width < Screen.height;
        _portraitSetting = new Setting();
        _portraitSetting.Position = _transform.position;
        _portraitSetting.Rotation = _transform.rotation.eulerAngles;
        _portraitSetting.FieldOfView = _camera.fieldOfView;
    }

    private void Start()
    {
        ChangeSettingCamera();
    }

    private void Update()
    {
        ChangeSettingCamera();
    }

    public Ray ScreenPointToRay(Vector2 position)
    {
        return _camera.ScreenPointToRay(position);
    } 

    private void ChangeSettingCamera()
    {
        if (_isPortraitOrientation == Screen.width < Screen.height)
            return;

        Setting setting;
        _isPortraitOrientation = !_isPortraitOrientation;

        if (_isPortraitOrientation)
            setting = _portraitSetting;
        else
            setting = _horisontalSetting;

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
