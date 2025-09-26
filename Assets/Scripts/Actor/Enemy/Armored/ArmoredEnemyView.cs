using System;
using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public class ArmoredEnemyView : MonoBehaviour, IEnemyView, IArmoredObject
{
    private IArmoredEnemyPresenter _presenter;
    private EnemyView _enemyView;

    public string Name => _enemyView.Name;

    private void Awake()
    {
        _enemyView = GetComponent<EnemyView>();
    }

    public void Initialize(IArmoredEnemyPresenter presenter)
    {
        _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
    }

    public void AddDebuff(IDebuff debaff) => _enemyView.AddDebuff(debaff);

    public void TakeDamage(IDamageAttributes damage) => _enemyView.TakeDamage(damage);

    public void IgnoreArmor(IDamageAttributes damage) => _presenter.IgnoreArmor(damage);

    public void PlayDamage() => _enemyView.PlayDamage();

    public void PlayMovement(bool isMovinng) => _enemyView.PlayMovement(isMovinng);

    public void PlayDead() => _enemyView.PlayDead();

    public void Destroy() => _enemyView.Destroy();
}
