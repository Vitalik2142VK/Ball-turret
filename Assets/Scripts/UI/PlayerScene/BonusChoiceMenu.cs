using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScaleAnimatorUI), typeof(HiderUI), typeof(CanvasGroup))]
public class BonusChoiceMenu : MonoBehaviour, IBonusChoiceMenu
{
    private const int MaxCountBonusButtons = 3;

    [SerializeField] private BonusChoiceButton[] _bonusChoiceButtons;
    [SerializeField] private Pause _pause;
    [SerializeField] private Button _confirmationButton;

    private IBonusRandomizer _randomizer;
    private IAnimatorUI _animator;
    private HiderUI _hiderUI;
    private CanvasGroup _canvasGroup;

    public event Action BonusSelected;

    public IBonus SelectedBonus { get; private set; }

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
        _animator = GetComponent<IAnimatorUI>();
        _hiderUI = GetComponent<HiderUI>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }

    private void OnEnable()
    {
        _confirmationButton.onClick.AddListener(OnChoiceBonus);

        foreach (var button in _bonusChoiceButtons)
            button.Clicked += OnSelectButton;
    }

    private void OnDisable()
    {
        _confirmationButton.onClick.RemoveListener(OnChoiceBonus);

        foreach (var button in _bonusChoiceButtons)
            button.Clicked -= OnSelectButton;
    }

    public void Initialize()
    {
        for (int i = 0; i < _bonusChoiceButtons.Length; i++)
            _bonusChoiceButtons[i].Initialize(i);
    }

    public void SetBonusRandomizer(IBonusRandomizer randomizer)
    {
        _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
    }

    public void Enable()
    {
        _pause.Enable();
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
        _animator.Show();
        _hiderUI.Hide();
        _confirmationButton.interactable = false;

        FillButtons();
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
        SelectedBonus = button.Bonus;

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

    private void OnChoiceBonus()
    {
        BonusSelected?.Invoke();

        DisableAllButton();
        StartCoroutine(Close());
    }

    private void DisableAllButton()
    {
        foreach (var button in _bonusChoiceButtons)
            button.Disable();
    }

    private IEnumerator Close()
    {
        _animator.Hide();

        yield return _animator.GetYieldAnimation();

        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _hiderUI.Show();
        _pause.Disable();
    }
}
