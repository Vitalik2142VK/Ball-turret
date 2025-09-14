using System;
using UnityEngine;

public class ArmoredEnemyCreator : MonoBehaviour, IEnemyCreator
{
    [SerializeField] private EnemyCreator _enemyCreator;
    [SerializeField] private Scriptable.ArmorAttributes _armorAttributes;

    private void OnValidate()
    {
        if (_enemyCreator == null)
        {
            GameObject gameObject = new GameObject();
            _enemyCreator = gameObject.AddComponent<EnemyCreator>();
            gameObject.transform.parent = transform;
        }

        if (_armorAttributes == null)
            throw new ArgumentNullException(nameof(_armorAttributes));
    }

    public string Name => throw new NotImplementedException();

    private void Awake()
    {
        _armorAttributes.CalculateArmor();
    }

    public IEnemy Create(IActorHealthModifier healthModifier)
    {
        if (healthModifier == null)
            throw new ArgumentNullException(nameof(healthModifier));

        IEnemy enemy = _enemyCreator.Create(healthModifier);
        Armor armor = new Armor(enemy, _armorAttributes);
        EnemyView createdEnemyView = _enemyCreator.ConsumeCreatedEnemyView();
        ArmoredEnemy armoredEnemy = new ArmoredEnemy(enemy, armor);

        if (createdEnemyView.TryGetComponent(out ArmoredEnemyView armoredEnemyView) == false)
            throw new InvalidOperationException($"Object {nameof(createdEnemyView)} do not have a component <{nameof(ArmoredEnemyView)}>");

        armoredEnemyView.Initialize(armoredEnemy);

        return armoredEnemy;
    }
}