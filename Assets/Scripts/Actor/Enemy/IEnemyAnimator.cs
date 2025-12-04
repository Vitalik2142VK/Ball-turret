public interface IEnemyAnimator : IDamagedObjectAnimator
{
    public void PlayMovement(bool isRunning);

    public void PlayVictory(bool isWin);
}