using System;
using UnityEngine;

namespace CannonTurret.CameraControl
{
    [RequireComponent(typeof(Camera))]
    public class CameraAdapter : MonoBehaviour, ICameraAdapter
    {
        private const float MaxRaion = 1f;

        [SerializeField] private Setting _horisontalSetting;
        [SerializeField, Range(0f, 1f)] private float _minRaion = 0.4f;
        [SerializeField] private float _maxFieldOfView;
        [SerializeField] private float _minFieldOfView;

        [Header("Debug")]
        [SerializeField] private bool _isDebug = false;

        private Transform _transform;
        private Camera _camera;
        private Setting _portraitSetting;
        private float _height;
        private float _width;

        public event Action OrientationChanged;
        public event Action RatioChanged;

        public Vector3 Rotation => _transform.rotation.eulerAngles;

        public float CameraHeight => _transform.position.y;

        public bool IsPortraitOrientation { get; private set; }

        private void OnValidate()
        {
            if (_maxFieldOfView < _minFieldOfView)
                throw new InvalidOperationException($"{_minFieldOfView} cannot be greater than {_maxFieldOfView}.");
        }

        private void Awake()
        {
            _transform = transform;
            _camera = GetComponent<Camera>();
            IsPortraitOrientation = true;
            _portraitSetting = new Setting();
            _portraitSetting.Position = _transform.position;
            _portraitSetting.Rotation = _transform.rotation.eulerAngles;
            _portraitSetting.FieldOfView = _camera.fieldOfView;

            if (Screen.width == Screen.height)
                _horisontalSetting.FieldOfView = _maxFieldOfView;
        }

        private void Start()
        {
            CheckCameraOrientation();
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (_isDebug)
            {
                ChangeSettingCamera(_horisontalSetting);
            }
            else
            {
                CheckCameraOrientation();
                AdjustFieldOfView();
            }
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
            _height = 0;
            _width = 0;

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

        private void AdjustFieldOfView()
        {
            if (IsPortraitOrientation)
                return;

            if (_height == Screen.height && _width == Screen.width)
                return;

            _height = Screen.height;
            _width = Screen.width;
            float ratio = _height / _width;
            float verticalRatioNormalized = Mathf.InverseLerp(_minRaion, MaxRaion, ratio);
            _camera.fieldOfView = Mathf.Lerp(_minFieldOfView, _maxFieldOfView, verticalRatioNormalized);

            RatioChanged?.Invoke();
        }

        [Serializable]
        private struct Setting
        {
            public Vector3 Position;
            public Vector3 Rotation;
            public float FieldOfView;
        }
    }
}