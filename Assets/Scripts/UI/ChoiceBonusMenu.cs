using System;
using System.Reflection;
using UnityEngine;

public class ChoiceBonusMenu : MonoBehaviour, IMenu
{
    private const int MaxCountBonusButtons = 3;
    private const int IndexFirstBonus = 0;
    private const int IndexSecondBonus = 1;
    private const int IndexThirdBonus = 2;

    [SerializeField] private ChoiceBonusButton[] _choiceBonusButtons;
    [SerializeField] private Pause _pause;

    private GameObject _gameObject;
    private IBonusRandomizer _randomizer;
    private IBonus _selectedBonus;

    private void OnValidate()
    {
        if (_choiceBonusButtons == null || _choiceBonusButtons.Length != MaxCountBonusButtons)
            _choiceBonusButtons = new ChoiceBonusButton[MaxCountBonusButtons];

        foreach (var choiceBonusButton in _choiceBonusButtons)
            if (choiceBonusButton == null)
                throw new NullReferenceException(nameof(choiceBonusButton));

        if (_pause == null)
            throw new NullReferenceException(nameof(_pause));
    }

    private void Awake()
    {
        _gameObject = gameObject;
        _gameObject.SetActive(false);
    }

    public void Initialize(IBonusRandomizer randomizer)
    {
        _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
    }

    public void Enable()
    {
        _pause.Enable();
        _gameObject.SetActive(true);

        FillButtons();
    }

    public void SelectFirstBonus()
    {
        SelectButton(IndexFirstBonus);
    }

    public void SelectSecondBonus()
    {
        SelectButton(IndexSecondBonus);
    }

    public void SelectThirdBonus()
    {
        SelectButton(IndexThirdBonus);
    }

    public void ChoiceBonus()
    {
        DisableAllButton();

        _selectedBonus.Activate();
        _gameObject.SetActive(false);
        _pause.Disable();
    }

    private void FillButtons()
    {
        int currentIndexButton = 0;
        var bonuses = _randomizer.GetBonuses(_choiceBonusButtons.Length);

        foreach (var bonus in bonuses)
        {
            var button = _choiceBonusButtons[currentIndexButton++];
            button.SetBonus(bonus);
            button.Enable();
        }
    }

    private void SelectButton(int index)
    {
        ChoiceBonusButton button = _choiceBonusButtons[index];
        _selectedBonus = button.Bonus;

        for (int i = 0; i < _choiceBonusButtons.Length; i++)
        {
            button = _choiceBonusButtons[i];

            if (i == index)
                button.Disable();
            else 
                button.Enable();
        }
    }

    private void DisableAllButton()
    {
        foreach (var button in _choiceBonusButtons)
            button.Disable();
    }
}
