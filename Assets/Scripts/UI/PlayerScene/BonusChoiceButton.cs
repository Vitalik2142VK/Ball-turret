using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ChoiceButton), typeof(ScaleButtonAnimator), typeof(Button))]
public class BonusChoiceButton : MonoBehaviour, IChoiceButton
{
    [SerializeField] private Scriptable.LocalizationData _localizationData;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Button _infoButton;

    private IChoiceButton _choiceButton;
    private IBonus _bonus;
    private IButtonAnimator _animator;
    private Button _button;
    private int _index;

    public event Action<int> Clicked;

    public IBonus Bonus => _bonus;

    private void OnValidate()
    {
        if (_localizationData == null)
            throw new NullReferenceException(nameof(_localizationData));

        if (_icon == null)
            throw new NullReferenceException(nameof(_icon));

        if (_description == null)
            throw new NullReferenceException(nameof(_description));

        if (_infoButton == null)
            throw new NullReferenceException(nameof(_infoButton));
    }

    private void Awake()
    {
        _animator = GetComponent<IButtonAnimator>();
        _choiceButton = GetComponent<ChoiceButton>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _infoButton.onClick.AddListener(OnChangeImageDiscripcion);
        _button.onClick.AddListener(OnClick);
        SetActiveDiscription(false);
    }

    private void OnDisable()
    {
        _infoButton.onClick.AddListener(OnChangeImageDiscripcion);
        _button.onClick.AddListener(OnClick);
    }

    public void Initialize(int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        _index = index;
    }

    public void Disable() => _choiceButton.Disable();

    public void SetBonus(IBonus bonus)
    {
        _bonus = bonus ?? throw new ArgumentNullException(nameof(_bonus));
        IBonusCard bonusCard = _bonus.BonusCard;

        _icon.sprite = bonusCard.Icon;
        _description.text = bonusCard.GetDescription(_localizationData.Language);
    }

    public void Enable()
    {
        _animator.PressOut();
        _choiceButton.Enable();
    }

    private void OnClick()
    {
        _animator.Press();

        Clicked?.Invoke(_index);
    }

    private void OnChangeImageDiscripcion()
    {
        bool isActiveImage = _icon.gameObject.activeSelf;

        SetActiveDiscription(isActiveImage);
    }

    private void SetActiveDiscription(bool isActive)
    {
        _description.gameObject.SetActive(isActive);
        _icon.gameObject.SetActive(isActive == false);
    }
}
