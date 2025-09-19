using System;
using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public class ArmoredEnemyView : MonoBehaviour, IEnemyView, IArmoredObject
{
    private IArmoredObject _armoredEnemy;
    private EnemyView _enemyView;

    public string Name => _enemyView.Name;
    public IActor Actor => _enemyView.Actor;

    private void OnValidate()
    {
        if (TryGetComponent(out EnemyView enemyView))
            _enemyView = enemyView;
        else
            throw new NullReferenceException(nameof(_enemyView));
    }

    private void Awake()
    {
        _enemyView = GetComponent<EnemyView>();
    }

    public void Initialize(IArmoredObject armoredEnemy)
    {
        _armoredEnemy = armoredEnemy ?? throw new ArgumentNullException(nameof(armoredEnemy));
    }

    public void TakeDamage(IDamageAttributes damage) => _enemyView.TakeDamage(damage);

    public void IgnoreArmor(IDamageAttributes damage) => _armoredEnemy.IgnoreArmor(damage);

    public void PlayDamage() => _enemyView.PlayDamage();

    public void PlayMovement() => _enemyView.PlayMovement();

    public void Destroy() => _enemyView.Destroy();
}