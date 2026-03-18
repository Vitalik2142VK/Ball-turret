using CannonTurret.Actors.MoveSystem;
using System;
using UnityEngine;

namespace CannonTurret.Actors.Bonuses
{
    public class CollisionBonus : IViewableBonus
    {
        private readonly IBonus Bonus;
        private readonly IBonusPresenter Presenter;
        private readonly IMovableObject Mover;

        public CollisionBonus(IBonus bonus, IBonusPresenter presenter, IMovableObject mover)
        {
            Bonus = bonus ?? throw new ArgumentNullException(nameof(bonus));
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            Mover = mover ?? throw new ArgumentNullException(nameof(mover));

            IsEnable = true;
        }
        public IBonusCard BonusCard => Bonus.BonusCard;

        public bool IsFinished => Mover.IsFinished;

        public bool IsEnable { get; private set; }

        public void Activate() => Bonus.Activate();

        public void SetStartPosition(Vector3 startPosition) => Mover.SetStartPosition(startPosition);

        public void EstablishPoint(Vector3 distance, float speed) => Mover.EstablishPoint(distance, speed);

        public void Move() => Mover.Move();

        public void HandleBonusGatherer(IBonusGatherer bonusGatherer)
        {
            bonusGatherer.Gather(Bonus);
            IsEnable = false;
        }

        public void Destroy()
        {
            Presenter.Destroy();
            IsEnable = false;
        }
    }
}