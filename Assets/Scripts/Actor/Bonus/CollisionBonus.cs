using System;
using UnityEngine;

public class CollisionBonus : IViewableBonus
{
    private IBonus _bonus;
    private IBonusView _view;
    private IMovableObject _mover;

    public IBonusCard BonusCard => _bonus.BonusCard;

    public CollisionBonus(IBonus bonus, IBonusView view, IMovableObject mover)
    {
        _bonus = bonus ?? throw new ArgumentNullException(nameof(bonus));
        _view = view ?? throw new ArgumentNullException(nameof(view));
        _mover = mover ?? throw new ArgumentNullException(nameof(mover));
    }

    public bool IsFinished => _mover.IsFinished;

    public bool IsEnable { get; private set; }

    public void Initialize(IBonusActivator bonusActivator) => _bonus.Initialize(bonusActivator);

    public void Activate() => _bonus.Activate();

    public void SetStartPosition(Vector3 startPosition) => _mover.SetStartPosition(startPosition);

    public void SetPoint(Vector3 distance, float speed) => _mover.SetPoint(distance, speed);

    public void Move() => _mover.Move();

    public void Destroy() => _view.Destroy();

    public IBonusActivator GetCloneBonusActivator() => _bonus.GetCloneBonusActivator();

    public void Enable()
    {
        IsEnable = true;
    }

    public void Disable()
    {
        IsEnable = false;
    }

    public void HandleBonusGatherer(IBonusGatherer bonusGatherer)
    {
        bonusGatherer.Gather(this);

        _view.PlayTaking();
        _view.Destroy();
    }
}
