using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DamagedObjectAnimator : MonoBehaviour, IDamagedObjectAnimator
{
    private const string GetHit = nameof(GetHit);
    private const string Die = nameof(Die);

    private Animator _animator;
    private int _hashGetHit;
    private int _hashDie;

    public float TimeCompletionDeath { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _hashGetHit = Animator.StringToHash(GetHit);
        _hashDie = Animator.StringToHash(Die);

        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        AnimationClip dieClip = System.Array.Find(clips, c => c.name == Die);
        TimeCompletionDeath = dieClip.length / _animator.speed;
    }

    public void PlayHit() => _animator.SetTrigger(_hashGetHit);

    public void PlayDead() => _animator.SetTrigger(_hashDie);
}
