using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private const string GetHit = nameof(GetHit);
    private const string Run = nameof(Run);
    private const string Die = nameof(Die);

    private Animator _animator;
    private int _hashGetHit;
    private int _hashRun;
    private int _hashDie;

    public float TimeCompletionDeath { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _hashGetHit = Animator.StringToHash(GetHit);
        _hashRun = Animator.StringToHash(Run);
        _hashDie = Animator.StringToHash(Die);
    }

    public void PlayHit() => _animator.SetTrigger(_hashGetHit);

    public void PlayMovement(bool isRunning) => _animator.SetBool(_hashRun, isRunning);

    public void PlayDead()
    {
        _animator.SetTrigger(_hashDie);
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        TimeCompletionDeath = stateInfo.length / _animator.speed;
    }
}