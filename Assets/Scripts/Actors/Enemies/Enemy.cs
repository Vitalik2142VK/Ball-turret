using CannonTurret.Actors.Debuffs;
using CannonTurret.Actors.MoveSystem;
using CannonTurret.DamageSystem;
using CannonTurret.Effects;
using CannonTurret.HealthSystem;
using System;
using UnityEngine;

namespace CannonTurret.Actors.Enemies
{
    public class Enemy : IEnemy
    {
        private readonly IEnemyPresenter Presenter;
        private readonly IDebuffHandler DebuffReceiver;
        private readonly IMovableObject Mover;
        private readonly IDamage Damage;
        private readonly IHealth Health;

        public Enemy(IEnemyPresenter presenter, IDebuffHandler debuffReceiver, IMovableObject mover, IDamage damage, IHealth health)
        {
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            DebuffReceiver = debuffReceiver ?? throw new ArgumentNullException(nameof(debuffReceiver));
            Mover = mover ?? throw new ArgumentNullException(nameof(mover));
            Damage = damage ?? throw new ArgumentNullException(nameof(damage));
            Health = health ?? throw new ArgumentNullException(nameof(health));

            Enable();
        }

        public bool IsFinished => Mover.IsFinished;

        public bool IsEnable { get; private set; }

        public void AddDebuff(IDebuff debaff) => DebuffReceiver.AddDebuff(debaff);

        public void ApplyDamage(IDamagedObject damagedObject) => Damage.Apply(damagedObject);

        public void SetStartPosition(Vector3 startPosition) => Mover.SetStartPosition(startPosition);

        public void EstablishPoint(Vector3 distance, float speed) => Mover.EstablishPoint(distance, speed);

        public void Win() => Presenter.Win();

        public void Move()
        {
            Mover.Move();
            Presenter.Move();
        }

        public void ActivateDebuffs()
        {
            DebuffReceiver.ActivateDebuffs();
            DebuffReceiver.RemoveCompletedDebuffs();
        }

        public void TakeDamage(IDamageAttributes damage)
        {
            Health.TakeDamage(damage);

            if (Health.IsAlive == false)
                IsEnable = false;
        }

        public void Destroy()
        {
            IsEnable = false;
            DebuffReceiver.Clean();
            Presenter.Destroy();
        }

        private void Enable()
        {
            IsEnable = true;
            Health.Restore();
        }
    }
}