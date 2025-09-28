using System;

public class EnemyPresenter : IEnemyPresenter
{
    private IEnemy _model;
    private IEnemyView _view;

    public void Initialize(IEnemy model, IEnemyView view)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _view = view ?? throw new ArgumentNullException(nameof(view));
    }

    public void AddDebuff(IDebuff debaff) => _model.AddDebuff(debaff);

    public void Destroy() => _view.Destroy();

    public void FinishDeath() => _model.Destroy();

    public void PrepareDeleted(IRemovedActorsCollector removedCollector)
    {
        removedCollector.Add(_model);
    }

    public void PrepareAttacked(IAttackingEnemiesCollector attackingCollector)
    {
        attackingCollector.Add(_model);
    }

    public void TakeDamage(IDamageAttributes damage)
    {
        _model.TakeDamage(damage);

        if (_model.IsEnable)
            _view.PlayDamage();
        else
            _view.PlayDead();
    }

    public void Move()
    {
        var isMovement = _model.IsFinished == false;
        _view.PlayMovement(isMovement);
    }
}