using CannonTurret.Actors.MoveSystem;
using System;
using UnityEngine;

namespace CannonTurret.Actors.Bonuses
{
    public class CollisionBonus : IViewableBonus
    {
        private readonly IBonus _bonus;
        private readonly IBonusPresenter _presenter;
        private readonly IMovableObject _mover;

        public CollisionBonus(IBonus bonus, IBonusPresenter presenter, IMovableObject mover)
        {
            _bonus = bonus ?? throw new ArgumentNullException(nameof(bonus));
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _mover = mover ?? throw new ArgumentNullException(nameof(mover));

            IsEnable = true;
        }
        public IBonusCard BonusCard => _bonus.BonusCard;

        public bool IsFinished => _mover.IsFinished;

        public bool IsEnable { get; private set; }

        public void Activate() => _bonus.Activate();

        public void SetStartPosition(Vector3 startPosition) => _mover.SetStartPosition(startPosition);

        public void EstablishPoint(Vector3 distance, float speed) => _mover.EstablishPoint(distance, speed);

        public void Move() => _mover.Move();

        public void HandleBonusGatherer(IBonusGatherer bonusGatherer)
        {
            bonusGatherer.Gather(_bonus);
            IsEnable = false;
        }

        public void Destroy()
        {
            _presenter.Destroy();
            IsEnable = false;
        }
    }
}