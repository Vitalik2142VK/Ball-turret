using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTouchInput : MonoBehaviour
{
    [SerializeField, Min(10f)] private float _maxDistanceRay = 100f;
    [SerializeField] private LayerMask _layerMask;

    private Camera _camera;
    private Touchscreen _touchscreen;

    public event Action PressFinished;

    public Vector3 TouchPositionInMap { get; private set; }
    public bool IsPress { get; private set; }

    private void Awake()
    {
        _camera = Camera.main;
        _touchscreen = Touchscreen.current;
    }

    public void LaunchTouchscreenToMap()
    {
        bool isPress = _touchscreen.primaryTouch.press.isPressed;

        if (isPress)
        {
            IsPress = isPress;

            FindTouchPosition();
        }
        else
        {
            if (IsPress)
            {
                IsPress = _touchscreen.primaryTouch.press.isPressed;

                PressFinished?.Invoke();
            }
        }
    }

    private void FindTouchPosition()
    {
        if (TryFindPositionInMap(out Vector3 positionMap))
            TouchPositionInMap = positionMap;
    }

    private bool TryFindPositionInMap(out Vector3 position)
    {
        Vector3 touchPosition = _touchscreen.primaryTouch.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistanceRay, _layerMask, QueryTriggerInteraction.Ignore))
        {
            position = hit.point;

            return true;
        }

        position = Vector3.zero;

        return false;
    }
}
