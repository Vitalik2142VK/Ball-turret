using System;
using UnityEngine;

public class CollisionBonus : IViewableBonus
{
    private IBonus _bonus;
    private IBonusPresenter _presenter;
    private IMovableObject _mover;

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

    public void SetPoint(Vector3 distance, float speed) => _mover.SetPoint(distance, speed);

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
