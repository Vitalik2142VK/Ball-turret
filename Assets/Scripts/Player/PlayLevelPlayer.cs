using System;
using UnityEngine;

[RequireComponent(typeof(PlayerTouchInput))]
public class PlayLevelPlayer : MonoBehaviour, IPlayer, IDesignator
{
    [SerializeField] private Scriptable.CachedPlayer _cachedUser;

    private PlayerTouchInput _touchInput;
    private ITurret _turret;

    public IWallet Wallet => _cachedUser.Wallet;
    public ITurretImprover TurretImprover => _cachedUser.TurretImprover;
    public float HealthCoefficient => _cachedUser.HealthCoefficient;
    public float DamageCoefficient => _cachedUser.DamageCoefficient;
    public int AchievedLevelIndex => _cachedUser.AchievedLevelIndex;
    public bool AreAdsDisabled => _cachedUser.AreAdsDisabled;

    private void OnValidate()
    {
        if (_cachedUser == null)
            throw new ArgumentNullException(nameof(_cachedUser));
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
            _turret.SetTouchPoint(_touchInput.TouchPositionInMap);
    }

    private void OnFinishPress()
    {
        if (_turret.IsReadyShoot == false)
            return;

        _turret.FixTargetPostion(_touchInput.TouchPositionInMap);
    }

    public void IncreaseAchievedLevel() => _cachedUser.IncreaseAchievedLevel();
}
