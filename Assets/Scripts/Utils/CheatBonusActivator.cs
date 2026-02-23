using UnityEngine;

public class CheatBonusActivator : MonoBehaviour
{
    [SerializeField, SerializeIterface(typeof(IBonusCreator))] private GameObject _bonusCreator;
    [SerializeField] private KeyCode _ativateKey = KeyCode.Q;

    private IBonus _bonus;

    private void OnValidate()
    {
        if (_bonusCreator == null)
            throw new System.NullReferenceException(nameof(_bonusCreator));
    }

    private void Update()
    {
        if (Input.GetKeyUp(_ativateKey))
            if (_bonus == null)
                CreatBonus();
            else
                _bonus.Activate();
    }

    private void CreatBonus()
    {
        IBonusCreator bonusCreator = _bonusCreator.GetComponent<IBonusCreator>();
        _bonus = bonusCreator.Create();
    }
}