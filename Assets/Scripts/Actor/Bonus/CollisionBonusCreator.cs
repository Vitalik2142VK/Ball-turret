using System;
using UnityEngine;

public class CollisionBonusCreator : MonoBehaviour, IViewableBonusCreator
{
    [SerializeField, SerializeIterface(typeof(IBonusCreator))] private GameObject _bonusCreator;
    [SerializeField] private BonusView _bonusPrefab;
    [SerializeField] private Sound _takedSound;

    private IBonusCreator _creator;

    public string Name => _bonusPrefab.name;

    private void OnValidate()
    {
        if (_bonusCreator == null)
            throw new NullReferenceException(nameof(_bonusCreator));

        if (_bonusPrefab == null)
            throw new NullReferenceException(nameof(_bonusPrefab));

        if (_takedSound == null)
            throw new NullReferenceException(nameof(_takedSound));
    }

    private void Awake()
    {
        _creator ??= _bonusCreator.GetComponent<IBonusCreator>();
    }

    public void Initialize(IBonusActivator bonusActivator) => _creator.Initialize(bonusActivator);

    public IBonus Create() 
    {
        _creator ??= _bonusCreator.GetComponent<IBonusCreator>();

        return _creator.Create();
    }

    public IViewableBonus Create(IBonus bonus)
    {
        bonus ??= Create();

        BonusView view = Instantiate(_bonusPrefab, Vector3.zero, _bonusPrefab.transform.rotation);
        Mover mover = new Mover(view.transform);
        CollisionBonus model = new CollisionBonus(bonus, view, mover);
        view.Initialize(model, _takedSound);

        return model;
    }
}