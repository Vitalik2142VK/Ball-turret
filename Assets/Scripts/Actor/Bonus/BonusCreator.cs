using UnityEngine;

public class BonusCreator : MonoBehaviour, IBonusCreator
{
    [SerializeField] private Scriptable.BonusCard _bonusCard;

    private void OnValidate()
    {
        if (_bonusCard == null)
            throw new System.NullReferenceException(nameof(_bonusCard));
    }

    public IBonus Create()
    {
        return new Bonus(_bonusCard);
    }
}
