using System;
using UnityEngine;

[RequireComponent(typeof(PlayerScreenPointer))]
public class PlayerController : MonoBehaviour, IPlayerController
{
    private ITurret _turret;
    private IPlayerScreenPointer _screenPointer;

    private void Awake()
    {
        _screenPointer = GetComponent<PlayerScreenPointer>();
    }

    private void OnEnable()
    {
        _screenPointer.PressFinished += OnFinishPress;
    }

    private void OnDisable()
    {
        _screenPointer.PressFinished -= OnFinishPress;
    }

    public void Initialize(ITurret turret)
    {
        _turret = turret ?? throw new ArgumentNullException(nameof(turret));
    }

    public void SelectTarget()
    {
        _screenPointer.UpdateInput();

        if (_screenPointer.IsPress && _turret.IsReadyShoot)
            _turret.SetTouchPoint(_screenPointer.TouchPositionInMap);
    }

    private void OnFinishPress()
    {
        if (_turret.IsReadyShoot == false)
            return;

        _turret.FixTargetPostion(_screenPointer.TouchPositionInMap);
    }
}
