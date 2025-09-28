using System;

public class BonusPresenter : IBonusPresenter
{
    private IViewableBonus _model;
    private IBonusView _view;

    public void Initialize(IViewableBonus model, IBonusView view)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _view = view ?? throw new ArgumentNullException(nameof(view));
    }

    public void HandleBonusGatherer(IBonusGatherer bonusGathering)
    {
        _model.HandleBonusGatherer(bonusGathering);
        _view.PlayTaking();

        if (_model.IsEnable == false)
            Destroy();
    }

    public void PrepareDeleted(IRemovedActorsCollector removedCollector)
    {
        removedCollector.Add(_model);
    }

    public void Destroy() => _view.Destroy();
}