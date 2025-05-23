using System;
using UnityEngine;
using UnityEngine.UI;

public class ImprovementMenu : MonoBehaviour
{
    private const int MaxCountImprovementButtons = 2;

    [SerializeField] private ImprovementChoiseButton[] _improvementChoiseButtons;
    [SerializeField] private Button _updateButton;

    private IMenu _previousMenu;
    private IGamePayTransaction _selectTransaction;
    private IImprovementShop _improvementShop;
    private GameObject _gameObject;

    private void OnValidate()
    {
        if (_improvementChoiseButtons == null || _improvementChoiseButtons.Length != MaxCountImprovementButtons)
            _improvementChoiseButtons = new ImprovementChoiseButton[MaxCountImprovementButtons];

        foreach (var button in _improvementChoiseButtons)
            if (button == null)
                throw new NullReferenceException(nameof(button));

        if (_updateButton == null)
            throw new NullReferenceException(nameof(_updateButton));
    }

    private void Awake()
    {
        _gameObject = gameObject;
        _gameObject.SetActive(false);
        _updateButton.interactable = false;
    }

    private void OnEnable()
    {
        foreach (var button in _improvementChoiseButtons)
            button.Clicked += OnSelectButton;
    }

    private void OnDisable()
    {
        foreach (var button in _improvementChoiseButtons)
            button.Clicked -= OnSelectButton;
    }

    public void Initialize(IImprovementShop improvementShop)
    {
        _improvementShop = improvementShop ?? throw new ArgumentNullException(nameof(improvementShop));

        for (int i = 0; i < _improvementChoiseButtons.Length; i++)
            _improvementChoiseButtons[i].SetIndex(i);
    }

    public void OnImprove()
    {
        if (_improvementShop.TryMakeTransaction(_selectTransaction))
        {
            foreach (var button in _improvementChoiseButtons)
                button.Enable();
        }
        else
        {
            Debug.Log("Transaction failed");
        }

        _selectTransaction = null;
        _updateButton.interactable = false;
    }

    public void OnClose()
    {
        _updateButton.interactable = false;
        _gameObject.SetActive(false);
        _previousMenu.Enable();
    }

    public void Open(IMenu previousMenu)
    {
        _previousMenu = previousMenu ?? throw new ArgumentNullException(nameof(previousMenu));
        _gameObject.SetActive(true);
        _updateButton.interactable = false;

        foreach (var button in _improvementChoiseButtons)
            button.Enable();
    }

    private void OnSelectButton(IGamePayTransaction gamePayTransaction, int index)
    {
        _selectTransaction = gamePayTransaction;

        ImprovementChoiseButton button;

        for (int i = 0; i < _improvementChoiseButtons.Length; i++)
        {
            button = _improvementChoiseButtons[i];

            if (i == index)
                button.Disable();
            else
                button.Enable();
        }

        _updateButton.interactable = true;
    }
}