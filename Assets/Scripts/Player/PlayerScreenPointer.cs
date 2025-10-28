using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScreenPointer : MonoBehaviour, IPlayerScreenPointer
{
    [SerializeField] private CameraAdapter _cameraAdapter;
    [SerializeField, Min(10f)] private float _maxDistanceRay = 100f;
    [SerializeField] private LayerMask _layerMask;

    private PlayerInput _input;
    private EventSystem _eventSystem;

    public event Action PressFinished;

    public Vector3 TouchPositionInMap { get; private set; }
    public bool IsPress { get; private set; }

    private void OnValidate()
    {
        if (_cameraAdapter == null)
            throw new NullReferenceException(nameof(_cameraAdapter));
    }

    private void Awake()
    {
        _input = new PlayerInput();
        _eventSystem = EventSystem.current;

        if (_eventSystem == null)
            throw new NullReferenceException(nameof(EventSystem.current));
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public void UpdateInput()
    {
        bool isPress = _input.Player.Press.IsPressed();

        if (isPress)
        {
            Vector2 touchPosition = _input.Player.Position.ReadValue<Vector2>();

            if (IsPointerOverUI(touchPosition))
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

    private bool IsPointerOverUI(Vector2 touchPosition)
    {
        var eventData = new PointerEventData(_eventSystem)
        {
            position = touchPosition
        };

        var results = new List<RaycastResult>();
        _eventSystem.RaycastAll(eventData, results);

        return results.Count > 0;
    }
}
