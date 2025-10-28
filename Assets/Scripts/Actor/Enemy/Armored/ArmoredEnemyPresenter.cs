using System;

public class ArmoredEnemyPresenter : IArmoredEnemyPresenter
{
    private IArmoredObject _armoredModel;
    private IEnemy _model;
    private IEnemyView _view;

    public ArmoredEnemyPresenter(IEnemy model, IEnemyView view)
    {
        if (model is IArmoredObject armoredModel)
            _armoredModel = armoredModel;
        else
            throw new ArgumentException($"<{nameof(model)}> must implement {nameof(IArmoredObject)}");

        _model = model ?? throw new ArgumentNullException(nameof(model));
        _view = view ?? throw new ArgumentNullException(nameof(view));
    }

    public void IgnoreArmor(IDamageAttributes damage)
    {
        _armoredModel.IgnoreArmor(damage);

        if (_model.IsEnable)
            _view.PlayDamage();
        else
            _view.PlayDead();
    }
}