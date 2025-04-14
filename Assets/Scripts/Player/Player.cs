using System;
using UnityEngine;

[RequireComponent(typeof(PlayerTouchInput))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private Scriptable.DamageImproverAttributes _damageImproverAttributes;

    private PlayerTouchInput _touchInput;
    private ITurret _turret;

    public IDamageImproverAttributes DamageImproverAttributes => _damageImproverAttributes;

    private void OnValidate()
    {
        if (_damageImproverAttributes == null)
            throw new ArgumentNullException(nameof(_damageImproverAttributes));
    }

    private void Awake()
    {
        _touchInput = GetComponent<PlayerTouchInput>();
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

        if (_touchInput.IsPress && _turret.IsReadyShoot)
        {
            Vector3 touchMapPosition = _touchInput.TouchPositionInMap;

            _turret.SetTouchPoint(touchMapPosition);
        }
    }

    private void OnFinishPress()
    {
        if (_turret.IsReadyShoot == false)
            return;

        _turret.FixTargetPostion();
    }
}
