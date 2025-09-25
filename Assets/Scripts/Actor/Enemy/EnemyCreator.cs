using System;
using UnityEngine;

public class EnemyCreator : MonoBehaviour, IEnemyCreator
{
    [SerializeField] private EnemyView _enemyPrefab;
    [SerializeField] private Scriptable.EnemyAttributes _enemyAttributes;
    [SerializeField] private ActorAudioController _audioController;

    private EnemyView _createdEnemyView;

    public string Name => _enemyPrefab.Name;

    private void OnValidate()
    {
        if (_enemyPrefab == null)
            throw new ArgumentNullException(nameof(_enemyPrefab));

        if (_enemyAttributes == null)
            throw new ArgumentNullException(nameof(_enemyAttributes));

        if (_audioController == null)
            throw new NullReferenceException(nameof(_audioController));
    }

    public IEnemy Create(IActorHealthModifier healthModifier)
    {
        if (healthModifier == null)
            throw new ArgumentNullException(nameof(healthModifier));

        _createdEnemyView = Instantiate(_enemyPrefab, Vector3.zero, _enemyPrefab.transform.rotation);
        HealthImprover healthImprover = new HealthImprover(_enemyAttributes);
        healthImprover.Improve(healthModifier.HealthCoefficient);

        Damage damage = new Damage(_enemyAttributes);
        HealthBar healthBar = _createdEnemyView.HealthBar;
        Health health = new Health(healthImprover, healthBar);
        health.Restore();

        IDebuffHandler debuffReceiver = _createdEnemyView.DebuffReceiver;
        Mover mover = new Mover(_createdEnemyView.transform);
        Enemy model = new Enemy(_createdEnemyView, debuffReceiver, mover, damage, health);
        _createdEnemyView.Initialize(model, _audioController);

        return model;
    }

    public EnemyView ConsumeCreatedEnemyView()
    {
        EnemyView createdEnemyView = _createdEnemyView;
        _createdEnemyView = null;

        return createdEnemyView;
    }
}
