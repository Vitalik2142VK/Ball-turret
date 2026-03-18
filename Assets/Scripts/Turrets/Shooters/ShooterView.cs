using UnityEngine;

namespace CannonTurret.Turrets.Shooters
{
    [RequireComponent(typeof(Animator))]
    public class ShooterView : MonoBehaviour, IShooterView
    {
        private const string TakeCover = nameof(TakeCover);
        private const string Shot = nameof(Shot);
        private const string GetHit = nameof(GetHit);
        private const string Win = nameof(Win);
        private const string RunAway = nameof(RunAway);

        private Animator _animator;
        private int _takeCoverCasch;
        private int _shotCasch;
        private int _getHitCasch;
        private int _winCasch;
        private int _runAwayCasch;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.applyRootMotion = false;

            _takeCoverCasch = Animator.StringToHash(TakeCover);
            _shotCasch = Animator.StringToHash(Shot);
            _getHitCasch = Animator.StringToHash(GetHit);
            _winCasch = Animator.StringToHash(Win);
            _runAwayCasch = Animator.StringToHash(RunAway);
        }

        public void PlayRunAway() => _animator.SetTrigger(_runAwayCasch);

        public void PlayTakeDamage() => _animator.SetTrigger(_getHitCasch);

        public void PlayShot() => _animator.SetTrigger(_shotCasch);

        public void PlayTakeCover() => _animator.SetTrigger(_takeCoverCasch);

        public void PlayWin()
        {
            _animator.applyRootMotion = true;
            _animator.SetTrigger(_winCasch);
        }
    }
}