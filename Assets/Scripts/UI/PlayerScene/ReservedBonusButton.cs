using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(ReservedBonusView), typeof(IButtonAnimator))]
[RequireComponent(typeof(ChoiceButton))]
public class ReservedBonusButton : MonoBehaviour
{
    [SerializeField] private Scriptable.LocalizationData _localizationData;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private ActivationButton _activationButton;
    [SerializeField] private Button _infoButton;

    private IButtonAnimator _animator;
    private IChoiceButton _choiceButton;
    private IBonusCard _bonusCard;
    private IBonusReservator _bonusReservator;
    private Button _button;
    private ReservedBonusView _view;

    public event Action<int> Clicked;
    public event Action BonusActivated;

    public int Index { get; private set; }

    private void OnValidate()
    {
        if (_localizationData == null)
            throw new NullReferenceException(nameof(_localizationData));

        if (_icon == null)
            throw new NullReferenceException(nameof(_icon));

        if (_description == null)
            throw new NullReferenceException(nameof(_description));

        if (_activationButton == null)
            throw new NullReferenceException(nameof(_activationButton));

        if (_infoButton == null)
            throw new NullReferenceException(nameof(_infoButton));
    }

    private void Awake()
    {
        _animator = GetComponent<IButtonAnimator>();
        _choiceButton = GetComponent<ChoiceButton>();
        _button = GetComponent<Button>();
        _view = GetComponent<ReservedBonusView>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
        _infoButton.onClick.AddListener(OnChangeImageDiscripcion);
        _activationButton.Clicked += OnActivate;
        _activationButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
        _infoButton.onClick.RemoveListener(OnChangeImageDiscripcion);
        _activationButton.Clicked -= OnActivate;
    }

    public void Initialize(IBonusReservator bonusReservator, IBonusCard bonusCard, int buttonIndex)
    {
        if (buttonIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(buttonIndex));

        _bonusReservator = bonusReservator ?? throw new ArgumentNullException(nameof(bonusReservator));
        _bonusCard = bonusCard ?? throw new ArgumentNullException(nameof(bonusCard));

        Index = buttonIndex;

        _icon.sprite = _bonusCard.Icon;
        _description.text = _bonusCard.GetDescription(_localizationData.Language);

        SetActiveDiscription(false);
    }

    public void Enable()
    {
        if (_view.IsCanActivate)
        {
            _choiceButton.Enable();

            if (_animator.IsPressed)
                _animator.PressOut();

            if (_activationButton.gameObject.activeSelf)
                _activationButton.Hide();
        }
        else
        {
            _choiceButton.Disable();
        }
    }

    private void OnClick()
    {
        _activationButton.Show();
        _animator.Press();

        Clicked?.Invoke(Index);
    }

    private void OnChangeImageDiscripcion()
    {
        bool isActiveImage = _icon.gameObject.activeSelf;

        SetActiveDiscription(isActiveImage);
    }

    private void OnActivate()
    {
        _bonusReservator.ActivateBonus(_bonusCard.Name);

        BonusActivated?.Invoke();
    }

    private void SetActiveDiscription(bool isActive)
    {
        _description.gameObject.SetActive(isActive);
        _icon.gameObject.SetActive(isActive == false);
    }
}
