using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerTouchInput))]
public class Player : MonoBehaviour, IPlayer, IEndPointStep
{
    private PlayerTouchInput _touchInput;
    private ITurret _turret;
    private IEndStep _endStep;
    private WaitForSeconds _waitingTimeEndShooting;
    private float _waitingTime = 0.25f;

    private void Awake()
    {
        _touchInput = GetComponent<PlayerTouchInput>();
        _waitingTimeEndShooting = new WaitForSeconds(_waitingTime);
    }

    private void OnEnable()
    {
        _touchInput.PressFinished += OnFinishPress;
    }

    private void OnDisable()
    {
        _touchInput.PressFinished -= OnFinishPress;
    }

    public void Initialize(ITurret turret)
    {
        _turret = turret ?? throw new ArgumentNullException(nameof(turret));
    }

    public void SelectTarget()
    {
        _touchInput.LaunchTouchscreenToMap();

        if (_touchInput.IsPress && _turret.IsInProcessShooting == false)
        {
            Vector3 touchMapPosition = _touchInput.TouchPositionInMap;

            _turret.SetTouchPoint(touchMapPosition);
        }
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }

    private void OnFinishPress()
    {
        if (_turret.IsInProcessShooting)
            return;

        _turret.FixTargetPostion();

        if (_turret.IsInProcessShooting)
            StartCoroutine(WaitEndShooting());
    }

    private IEnumerator WaitEndShooting()
    {
        while (_turret.IsInProcessShooting)
        {
            yield return _waitingTimeEndShooting;
        }

        _endStep.End();
    }
}
