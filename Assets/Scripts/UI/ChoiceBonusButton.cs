using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class ChoiceBonusButton : MonoBehaviour
{
    private const float AphaEnable = 1f;
    private const float AphaDisable = 0.5f;

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _description;

    private CanvasGroup _canvasGroupButton;
    private IBonus _bonus;

    public IBonus Bonus => _bonus;

    private void OnValidate()
    {
        if (_icon == null)
            throw new NullReferenceException(nameof(_icon));

        if (_description == null)
            throw new NullReferenceException(nameof(_description));
    }

    private void Awake()
    {
        _canvasGroupButton = GetComponent<CanvasGroup>();
        _canvasGroupButton.blocksRaycasts = false;
    }

    public void SetBonus(IBonus bonus)
    {
        _bonus = bonus ?? throw new ArgumentNullException(nameof(_bonus));
        IBonusCard bonusCard = _bonus.BonusCard;

        _icon.sprite = bonusCard.Icon;
        _description.text = bonusCard.Description;
    }

    public void Enable()
    {
        _canvasGroupButton.alpha = AphaEnable;
        _canvasGroupButton.interactable = true;
        _canvasGroupButton.blocksRaycasts = true;
    }

    public void Disable()
    {
        _canvasGroupButton.alpha = AphaDisable;
        _canvasGroupButton.interactable = false;
        _canvasGroupButton.blocksRaycasts = false;
    }
}
