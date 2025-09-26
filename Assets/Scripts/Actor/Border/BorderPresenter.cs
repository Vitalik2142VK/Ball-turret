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

    public void Destroy() => _view.Destroy();

    public void FinishDeath() => _model.Destroy();

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

    private void ChoosePlayView()
    {
        if (_model.IsEnable)
            _view.PlayDamage();
        else
            _view.PlayDead();
    }

}