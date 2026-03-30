using CannonTurret.Actors.MoveSystem;
using CannonTurret.DamageSystem;
using CannonTurret.HealthSystem;
using System;
using UnityEngine;

namespace CannonTurret.Actors.Borders
{
    public class Border : IBorder
    {
        private readonly IBorderPresenter _presenter;
        private readonly IMovableObject _mover;
        private readonly IArmor _armor;
        private readonly IHealth _health;

        public Border(IBorderPresenter presenter, IMovableObject mover, IArmor armor, IHealth health)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _mover = mover ?? throw new ArgumentNullException(nameof(mover));
            _armor = armor ?? throw new ArgumentNullException(nameof(armor));
            _health = health ?? throw new ArgumentNullException(nameof(health));

            Enable();
        }

        public bool IsFinished => _mover.IsFinished;

        public bool IsEnable { get; private set; }

        public void SetStartPosition(Vector3 startPosition) => _mover.SetStartPosition(startPosition);

        public void EstablishPoint(Vector3 distance, float speed) => _mover.EstablishPoint(distance, speed);

        public void Move() => _mover.Move();

        public void TakeDamage(IDamageAttributes damage)
        {
            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            _armor.ReduceDamage(damage);

            CheckAlive();
        }

        public void IgnoreArmor(IDamageAttributes damage)
        {
            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            _health.TakeDamage(damage);

            CheckAlive();
        }

        public void Destroy()
        {
            _presenter.Destroy();
            IsEnable = false;
        }

        private void Enable()
        {
            IsEnable = true;

            _health.Restore();
        }

        private void CheckAlive()
        {
            if (_health.IsAlive == false)
                IsEnable = false;
        }
    }
}