using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShooterView : MonoBehaviour, IShooterView
{
    private const string TakeCover = nameof(TakeCover);
    private const string Shot = nameof(Shot);
    private const string GetHit = nameof(GetHit);
    private const string Win = nameof(Win);
    private const string RunAway = nameof(RunAway);

    [SerializeField] private RocketView _rocketView;

    private Animator _animator;
    private int _takeCoverCasch;
    private int _shotCasch;
    private int _getHitCasch;
    private int _winCasch;
    private int _runAwayCasch;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _takeCoverCasch = Animator.StringToHash(TakeCover);
        _shotCasch = Animator.StringToHash(Shot);
        _getHitCasch = Animator.StringToHash(GetHit);
        _winCasch = Animator.StringToHash(Win);
        _runAwayCasch = Animator.StringToHash(RunAway);
    }

    private void OnEnable()
    {
        if (_rocketView)
            _rocketView.RocketFinished += OnPlayTakeCover;
    }

    private void OnDisable()
    {
        if (_rocketView)
            _rocketView.RocketFinished -= OnPlayTakeCover;
    }

    public void PlayRunAway() => _animator.SetTrigger(_runAwayCasch);

    public void PlayTakeDamage() => _animator.SetTrigger(_getHitCasch);

    public void PlayShot() => _animator.SetTrigger(_shotCasch);

    public void PlayWin() => _animator.SetTrigger(_winCasch);

    private void OnPlayTakeCover() => _animator.SetTrigger(_takeCoverCasch);
}
