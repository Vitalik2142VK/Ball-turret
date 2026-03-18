using CannonTurret.CameraControl;
using CannonTurret.UI;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace CannonTurret.PlayerSystem
{
    public class PlayerScreenPointer : MonoBehaviour, IPlayerScreenPointer
    {
        [SerializeField] private CameraAdapter _cameraAdapter;
        [SerializeField] private CanvasPointerChecker _canvasPointerChecker;
        [SerializeField][Min(10f)] private float _maxDistanceRay = 100f;
        [SerializeField] private LayerMask _layerMask;

        private EventSystem _eventSystem;

        public event Action PressFinished;

        public Vector3 TouchPositionInMap { get; private set; }

        public bool IsPress { get; private set; }

        private void OnValidate()
        {
            if (_cameraAdapter == null)
                throw new NullReferenceException(nameof(_cameraAdapter));

            if (_canvasPointerChecker == null)
                throw new NullReferenceException(nameof(_canvasPointerChecker));
        }

        private void Awake()
        {
            _eventSystem = EventSystem.current;

            if (_eventSystem == null)
                throw new NullReferenceException(nameof(EventSystem.current));
        }

        public void UpdateInput()
        {
            if (Pointer.current == null)
                return;

            bool isPress = Pointer.current.press.isPressed;

            if (isPress)
            {
                Vector2 touchPosition = Pointer.current.position.ReadValue();

                if (_canvasPointerChecker.IsPointerOverUI(touchPosition))
                    return;

                IsPress = isPress;

                if (TryFindPositionInMap(out Vector3 position, touchPosition))
                    TouchPositionInMap = position;
            }
            else
            {
                if (IsPress)
                {
                    IsPress = false;

                    PressFinished?.Invoke();
                }
            }
        }

        private bool TryFindPositionInMap(out Vector3 position, Vector2 touchPosition)
        {
            Ray ray = _cameraAdapter.ScreenPointToRay(touchPosition);
            float maxDistanceRay = _maxDistanceRay + _cameraAdapter.CameraHeight;

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistanceRay, _layerMask, QueryTriggerInteraction.Ignore))
            {
                position = hit.point;

                return true;
            }

            position = Vector3.zero;

            return false;
        }
    }
}