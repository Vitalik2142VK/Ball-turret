using System;

public class BorderPresenter : IBorderPresenter
{
    private IBorder _model;
    private IBorderView _view;

    public void Initialize(IBorder model, IBorderView view)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _view = view ?? throw new ArgumentNullException(nameof(view));
    }

    public void FinishDeath() => _model.Destroy();

    public void PrepareDeleted(IRemovedActorsCollector removedCollector)
    {
        removedCollector.Add(_model);
    }

    public void TakeDamage(IDamageAttributes damage)
    {
        _model.TakeDamage(damage);

        ChoosePlayView();
    }

    public void IgnoreArmor(IDamageAttributes damage)
    {
        _model.IgnoreArmor(damage);

        ChoosePlayView();
    }

    public void Destroy()
    {
        if (_view.IsActive)
            _view.PlayDead();
        else
            _view.Destroy();
    }

    private void ChoosePlayView()
    {
        if (_model.IsEnable)
            _view.PlayDamage();
        else
            _view.PlayDead();
    }
}