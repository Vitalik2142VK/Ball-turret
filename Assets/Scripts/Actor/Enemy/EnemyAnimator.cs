using UnityEngine;

[RequireComponent(typeof(Animator), typeof(DamagedObjectAnimator))]
public class EnemyAnimator : MonoBehaviour, IEnemyAnimator
{
    private const string Run = nameof(Run);

    [SerializeField] private DamagedObjectAnimator _damagedObjectAnimator;

    private Animator _animator;
    private int _hashRun;

    public float TimeCompletionDeath => _damagedObjectAnimator.TimeCompletionDeath;

    private void OnValidate()
    {
        if (_damagedObjectAnimator == null)
            throw new System.NullReferenceException(nameof(_damagedObjectAnimator));
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _hashRun = Animator.StringToHash(Run);
    }

    public void PlayHit() => _damagedObjectAnimator.PlayHit();

    public void PlayDead() => _damagedObjectAnimator.PlayDead();

    public void PlayMovement(bool isRunning) => _animator.SetBool(_hashRun, isRunning);
}
