using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IAnimatorUI))]
public class ReservedBonusesWindow : MonoBehaviour, IReservedBonusesWindow
{
    [SerializeField] private ReservedBonusButton _reservedBonusButtonPrefab;
    [SerializeField] private ContentSizeFitter _content;
    [SerializeField] private Button _closeButton;
    [SerializeField] private OpenWindowButton _openButton;

    private IAnimatorUI _animator;
    private IBonusReservator _bonusReservator;
    private List<ReservedBonusButton> _reservedBonusButtons;

    public bool IsActive => gameObject.activeSelf;

    private void OnValidate()
    {
        if (_reservedBonusButtonPrefab == null)
            throw new NullReferenceException(nameof(_reservedBonusButtonPrefab));

        if (_content == null)
            throw new NullReferenceException(nameof(_content));

        if (_closeButton == null)
            throw new NullReferenceException(nameof(_closeButton));

        if (_openButton == null)
            throw new NullReferenceException(nameof(_openButton));
    }

    private void Awake()
    {
        _animator = GetComponent<IAnimatorUI>();
        gameObject.SetActive(false);
        _openButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(OnClose);

        if (_reservedBonusButtons != null)
            foreach (var button in _reservedBonusButtons)
            {
                button.Enable();
                button.Clicked += OnSelectButton;
                button.BonusActivated += OnClose;
            }
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnClose);

        if (_reservedBonusButtons != null)
            foreach (var button in _reservedBonusButtons)
            {
                button.Clicked -= OnSelectButton;
                button.BonusActivated -= OnClose;
            }
    }

    public void Initialize(IBonusReservator bonusReservator)
    {
        _bonusReservator = bonusReservator ?? throw new ArgumentNullException(nameof(bonusReservator));

        _reservedBonusButtons = new List<ReservedBonusButton>();
        int index = 0;

        foreach (var reservatedBonus in bonusReservator.Bonuses)
        {
            var button = Instantiate(_reservedBonusButtonPrefab);
            var reservedBonusView = button.GetComponent<IReservedBonusView>();

            reservatedBonus.Initialize(reservedBonusView);
            button.transform.SetParent(_content.transform);
            button.Initialize(_bonusReservator, reservatedBonus.BonusCard, index++);
            button.transform.localScale = _reservedBonusButtonPrefab.transform.localScale;

            _reservedBonusButtons.Add(button);
        }

        if (_reservedBonusButtons.Count <= 0)
            throw new InvalidOperationException($"IEnumerable '{nameof(bonusReservator)}' is empty");
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _animator.Show();
    }

    public void Hide()
    {
        if (gameObject.activeSelf)
            _animator.Hide();
    }

    private void OnClose()
    {
        _animator.Hide();

        StartCoroutine(WaitClosure());
    }

    private void OnSelectButton(int buttonIndex)
    {
        foreach (var button in _reservedBonusButtons)
            if (button.Index != buttonIndex)
                button.Enable();
    }

    private IEnumerator WaitClosure()
    {
        yield return _animator.GetYieldAnimation();

        gameObject.SetActive(false);

        if (_bonusReservator.IsBonusActivated == false && _bonusReservator.HasBonuses)
            _openButton.Show();
    }
}
