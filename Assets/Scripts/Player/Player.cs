using UnityEngine;

[RequireComponent(typeof(PlayerTouchInput))]
public class Player : MonoBehaviour
{
    private ITurret _turret;
    private PlayerTouchInput _touchInput;

    private void Awake()
    {
        _touchInput = GetComponent<PlayerTouchInput>();
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

    public void Initialize(ITurret turret)
    {
        _turret = turret ?? throw new System.ArgumentNullException(nameof(turret));
    }

    private void OnFinishPress()
    {
        _turret.FixTargetPostion();
    }
}
