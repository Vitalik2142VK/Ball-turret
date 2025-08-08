using System;
using UnityEngine;

public class PlayerScreenPointer : MonoBehaviour, IPlayerScreenPointer
{
    [SerializeField] private CameraAdapter _cameraAdapter;
    [SerializeField, Min(10f)] private float _maxDistanceRay = 100f;
    [SerializeField] private LayerMask _layerMask;

    private PlayerInput _input;

    public event Action PressFinished;

    public Vector3 TouchPositionInMap { get; private set; }
    public bool IsPress { get; private set; }

    private void OnValidate()
    {
        if (_cameraAdapter == null)
            throw new ArgumentNullException(nameof(_cameraAdapter));
    }

    private void Awake()
    {
        _input = new PlayerInput();
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
            IsPress = isPress;

            if (TryFindPositionInMap(out Vector3 position))
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

    private bool TryFindPositionInMap(out Vector3 position)
    {
        Vector2 touchPosition = _input.Player.Position.ReadValue<Vector2>();
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
