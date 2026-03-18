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
        private readonly IGun Gun;
        private readonly ITower Tower;
        private readonly IHealth Health;
        private readonly ITurretView View;

        private IEndStep _endStep;

        public event Action Fired;

        public Turret(ITurretView turretView, IGun gun, ITower tower, IHealth health)
        {
            View = turretView ?? throw new ArgumentNullException(nameof(turretView));
            Gun = gun ?? throw new ArgumentNullException(nameof(gun));
            Tower = tower ?? throw new ArgumentNullException(nameof(tower));
            Health = health ?? throw new ArgumentNullException(nameof(health));

            IsDestroyed = false;
        }

        public bool IsDestroyed { get; private set; }

        public bool IsReadyShoot => Gun.IsRecharged;

        public void Enable()
        {
            Gun.ShotExecuted += OnShoot;
            Gun.Reloaded += OnEndStep;
        }

        public void Disable()
        {
            Gun.ShotExecuted -= OnShoot;
            Gun.Reloaded -= OnEndStep;
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }

        public void SetTouchPoint(Vector3 touchPosition)
        {
            Tower.TakeAim(touchPosition);
        }

        public void FixTargetPostion(Vector3 targetPostion)
        {
            if (Tower.IsReadyShoot)
            {
                Tower.AimBeforeShooting(targetPostion);
                Gun.Shoot(Tower.Direction);

                Fired?.Invoke();
            }

            Tower.SaveDirection();
        }

        public void TakeDamage(IDamageAttributes damage)
        {
            Health.TakeDamage(damage);

            if (Health.IsAlive == false)
                Destroy();
            else
                View.PlayTakeDamage();
        }

        public void Destroy()
        {
            View.PlayDestroy();

            IsDestroyed = true;
        }

        private void OnEndStep()
        {
            Tower.ClearDirection();
            _endStep.End();
        }

        private void OnShoot() => View.PlayShoot();
    }
}