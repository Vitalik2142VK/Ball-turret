using CannonTurret.Actors.MoveSystem;
using CannonTurret.DamageSystem;
using CannonTurret.HealthSystem;
using System;
using UnityEngine;

namespace CannonTurret.Actors.Borders
{
    public class Border : IBorder
    {
        private readonly IBorderPresenter Presenter;
        private readonly IMovableObject Mover;
        private readonly IArmor Armor;
        private readonly IHealth Health;

        public Border(IBorderPresenter presenter, IMovableObject mover, IArmor armor, IHealth health)
        {
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            Mover = mover ?? throw new ArgumentNullException(nameof(mover));
            Armor = armor ?? throw new ArgumentNullException(nameof(armor));
            Health = health ?? throw new ArgumentNullException(nameof(health));

            Enable();
        }

        public bool IsFinished => Mover.IsFinished;

        public bool IsEnable { get; private set; }

        public void SetStartPosition(Vector3 startPosition) => Mover.SetStartPosition(startPosition);

        public void EstablishPoint(Vector3 distance, float speed) => Mover.EstablishPoint(distance, speed);

        public void Move() => Mover.Move();

        public void TakeDamage(IDamageAttributes damage)
        {
            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            Armor.ReduceDamage(damage);

            CheckAlive();
        }

        public void IgnoreArmor(IDamageAttributes damage)
        {
            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            Health.TakeDamage(damage);

            CheckAlive();
        }

        public void Destroy()
        {
            Presenter.Destroy();
            IsEnable = false;
        }

        private void Enable()
        {
            IsEnable = true;

            Health.Restore();
        }

        private void CheckAlive()
        {
            if (Health.IsAlive == false)
                IsEnable = false;
        }
    }
}