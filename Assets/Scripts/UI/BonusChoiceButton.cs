﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ChoiceButton))]
public class BonusChoiceButton : MonoBehaviour, IChoiceButton
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _description;

    private IChoiceButton _choiceButton;
    private IBonus _bonus;
    private int _index;

    public event Action<int> Clicked;

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
        _choiceButton = GetComponent<ChoiceButton>();
    }

    public void Initialize(int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        _index = index;
    }

    public void SetBonus(IBonus bonus)
    {
        _bonus = bonus ?? throw new ArgumentNullException(nameof(_bonus));
        IBonusCard bonusCard = _bonus.BonusCard;

        _icon.sprite = bonusCard.Icon;
        _description.text = bonusCard.Description;
    }

    public void OnClick()
    {
        Clicked?.Invoke(_index);
    }

    public void Enable()
    {
        _choiceButton.Enable();
    }

    public void Disable()
    {
        _choiceButton.Disable();
    }
}
