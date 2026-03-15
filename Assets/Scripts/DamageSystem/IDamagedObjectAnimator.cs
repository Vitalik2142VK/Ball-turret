public interface IDamagedObjectAnimator
{
    public float TimeCompletionDeath { get; }

    public void PlayHit();

    public void PlayDead();
}