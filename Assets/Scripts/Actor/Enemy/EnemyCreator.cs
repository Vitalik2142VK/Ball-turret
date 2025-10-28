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

        EnemyView view = Instantiate(_enemyPrefab, Vector3.zero, _enemyPrefab.transform.rotation);
        HealthImprover healthImprover = new HealthImprover(_enemyAttributes);
        healthImprover.Improve(healthModifier.HealthCoefficient);

        Damage damage = new Damage(_enemyAttributes);
        HealthBar healthBar = view.HealthBar;
        Health health = new Health(healthImprover, healthBar);
        health.Restore();

        IDebuffHandler debuffReceiver = view.DebuffReceiver;
        Mover mover = new Mover(view.transform);
        EnemyPresenter presenter = new EnemyPresenter();
        Enemy model = new Enemy(presenter, debuffReceiver, mover, damage, health);
        view.Initialize(presenter, _audioController);
        presenter.Initialize(model, view);

        _createdEnemyView = view;

        return model;
    }

    public EnemyView ConsumeCreatedEnemyView()
    {
        EnemyView createdEnemyView = _createdEnemyView;
        _createdEnemyView = null;

        return createdEnemyView;
    }
}
