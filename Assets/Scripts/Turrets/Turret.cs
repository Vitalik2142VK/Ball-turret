using CannonTurret.DamageSystem;
using CannonTurret.HealthSystem;
using CannonTurret.StepSystem;
using CannonTurret.Turrets.Guns;
using System;
using UnityEngine;

namespace CannonTurret.Turrets
{
    public class Turret : ITurret, IShotAction
    {
        private readonly IGun _gun;
        private readonly ITower _tower;
        private readonly IHealth _health;
        private readonly ITurretView _view;

        private IEndStep _endStep;

        public event Action Fired;

        public Turret(ITurretView turretView, IGun gun, ITower tower, IHealth health)
        {
            _view = turretView ?? throw new ArgumentNullException(nameof(turretView));
            _gun = gun ?? throw new ArgumentNullException(nameof(gun));
            _tower = tower ?? throw new ArgumentNullException(nameof(tower));
            _health = health ?? throw new ArgumentNullException(nameof(health));

            IsDestroyed = false;
        }

        public bool IsDestroyed { get; private set; }

        public bool IsReadyShoot => _gun.IsRecharged;

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

        public void TakeDamage(IDamageAttributes damage)
        {
            _health.TakeDamage(damage);

            if (_health.IsAlive == false)
                Destroy();
            else
                _view.PlayTakeDamage();
        }

        public void Destroy()
        {
            _view.PlayDestroy();

            IsDestroyed = true;
        }

        private void OnEndStep()
        {
            _tower.ClearDirection();
            _endStep.End();
        }

        private void OnShoot() => _view.PlayShoot();
    }
}