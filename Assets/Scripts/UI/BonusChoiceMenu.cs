using System;
using UnityEngine;
using UnityEngine.UI;

public class BonusChoiceMenu : MonoBehaviour, IMenu
{
    private const int MaxCountBonusButtons = 3;

    [SerializeField] private BonusChoiceButton[] _bonusChoiceButtons;
    [SerializeField] private Pause _pause;
    [SerializeField] private Button _confirmationButton;

    private IBonusRandomizer _randomizer;
    private IBonus _selectedBonus;

    private void OnValidate()
    {
        if (_bonusChoiceButtons == null || _bonusChoiceButtons.Length != MaxCountBonusButtons)
            _bonusChoiceButtons = new BonusChoiceButton[MaxCountBonusButtons];

        foreach (var button in _bonusChoiceButtons)
            if (button == null)
                throw new NullReferenceException(nameof(button));

        if (_pause == null)
            throw new NullReferenceException(nameof(_pause));

        if (_confirmationButton == null)
            throw new NullReferenceException(nameof(_confirmationButton));
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (var button in _bonusChoiceButtons)
            button.Clicked += OnSelectButton;
    }

    private void OnDisable()
    {
        foreach (var button in _bonusChoiceButtons)
            button.Clicked -= OnSelectButton;
    }

    public void Initialize(IBonusRandomizer randomizer)
    {
        _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));

        for (int i = 0; i < _bonusChoiceButtons.Length; i++)
            _bonusChoiceButtons[i].Initialize(i);
    }

    public void Enable()
    {
        _pause.Enable();
        gameObject.SetActive(true);
        _confirmationButton.interactable = false;

        FillButtons();
    }

    public void OnChoiceBonus()
    {
        DisableAllButton();

        _selectedBonus.Activate();
        gameObject.SetActive(false);
        _pause.Disable();
    }

    private void FillButtons()
    {
        int currentIndexButton = 0;
        var bonuses = _randomizer.GetBonuses(_bonusChoiceButtons.Length);

        foreach (var bonus in bonuses)
        {
            var button = _bonusChoiceButtons[currentIndexButton++];
            button.SetBonus(bonus);
            button.Enable();
        }
    }

    private void OnSelectButton(int index)
    {
        var button = _bonusChoiceButtons[index];
        _selectedBonus = button.Bonus;

        for (int i = 0; i < _bonusChoiceButtons.Length; i++)
        {
            button = _bonusChoiceButtons[i];

            if (i == index)
                button.Disable();
            else 
                button.Enable();
        }

        _confirmationButton.interactable = true;
    }

    private void DisableAllButton()
    {
        foreach (var button in _bonusChoiceButtons)
            button.Disable();
    }
}
