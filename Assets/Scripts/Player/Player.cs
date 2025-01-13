using System;
using UnityEngine;

[RequireComponent(typeof(PlayerTouchInput))]
public class Player : MonoBehaviour
{
    [SerializeField] private Turret _turret;

    private Transform _transform;
    private PlayerTouchInput _touchInput;

    private void OnValidate()
    {
        if (_turret == null)
            throw new NullReferenceException(nameof(_turret));
    }

    private void Awake()
    {
        _transform = transform;

        _touchInput = GetComponent<PlayerTouchInput>();

        TargetPoint targetPoint = GetTargetPoint();
        _turret.SetTargetPoint(targetPoint);
    }

    private void OnEnable()
    {
        _touchInput.PressFinished += OnFinishPress;
    }

    private void Update()
    {
        if (_touchInput.IsPress)
        {
            Vector3 touchMapPosition = _touchInput.TouchPositionInMap;

            _turret.SetTouchPoint(touchMapPosition);
        }
    }

    private void OnDisable()
    {
        _touchInput.PressFinished -= OnFinishPress;
    }

    private TargetPoint GetTargetPoint()
    {
        TargetPoint target = _transform.GetComponentInChildren<TargetPoint>();

        return target == null ? throw new InvalidOperationException() : target;
    }

    private void OnFinishPress()
    {
        _turret.FixTargetPostion();
    }
}
