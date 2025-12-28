using System;
using UnityEngine;

namespace RecorderLevel
{
    public class RecordingTurret : ITurret
    {
        private IGun _gun;
        private ITower _tower;
        private ITurretView _view;
        private IEndStep _endStep;

        public event Action Fired;

        public RecordingTurret(ITurretView turretView, IGun gun, ITower tower)
        {
            _view = turretView ?? throw new ArgumentNullException(nameof(turretView));
            _gun = gun ?? throw new ArgumentNullException(nameof(gun));
            _tower = tower ?? throw new ArgumentNullException(nameof(tower));
        }

        public bool IsReadyShoot => _gun.IsRecharged;
        public bool IsDestroyed => false;

        public void Enable()
        {
            _gun.ShotExecuted += OnShoot;
            _gun.Reloaded += OnEndStep;
        }

        public void Disable()
        {
            _gun.ShotExecuted -= OnShoot;
            _gun.Reloaded -= OnEndStep;
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }

        public void SetTouchPoint(Vector3 touchPosition)
        {
            _tower.TakeAim(touchPosition);
        }

        public void FixTargetPostion(Vector3 targetPostion)
        {
            if (_tower.IsReadyShoot)
            {
                _tower.AimBeforeShooting(targetPostion);
                _gun.Shoot(_tower.Direction);

                Fired?.Invoke();
            }

            _tower.SaveDirection();
        }

        private void OnEndStep()
        {
            _tower.ClearDirection();
            _endStep.End();
        }

        private void OnShoot() => _view.PlayShoot();

        public void TakeDamage(IDamageAttributes damage)
        {
            Debug.Log($"TakeDamage = {damage.Damage}");
        }
    }
}
