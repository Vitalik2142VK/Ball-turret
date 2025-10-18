using System;
using System.Linq;
using UnityEngine;

public class ChoiceBonusActivatorCreator : MonoBehaviour, IBonusActivatorCreator
{
    [SerializeField] private BonusChoiceMenu _bonusChoiceMenu;
    [SerializeField, SerializeIterface(typeof(IBonusCreator))] private GameObject[] _randomBonusCreators;

    private ChoiceBonusActivator _bonusActivators;

    private void OnValidate()
    {
        if (_bonusChoiceMenu == null)
            throw new NullReferenceException(nameof(_bonusChoiceMenu));

        if (_randomBonusCreators == null || _randomBonusCreators.Length == 0)
            throw new InvalidOperationException(nameof(_randomBonusCreators));

        foreach (var bonusConfigurator in _randomBonusCreators)
            if (bonusConfigurator == null)
                throw new NullReferenceException($"{_randomBonusCreators} contains null objects");
    }

    public IBonusActivator Create()
    {
        if (_bonusActivators == null)
        {
            var bonusPrefabs = _randomBonusCreators.Select(bc => bc.GetComponent<IBonusCreator>().Create()).ToArray();
            BonusRandomizer bonusRandomizer = new BonusRandomizer(bonusPrefabs);
            _bonusActivators = new ChoiceBonusActivator(_bonusChoiceMenu, bonusRandomizer);
        }

        return _bonusActivators;
    }

    public void SetBonusReservator(IBonusReservator bonusReservator) => _bonusActivators.SetBonusReservator(bonusReservator);
}
