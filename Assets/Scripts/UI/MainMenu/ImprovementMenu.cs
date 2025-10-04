using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MenuAnimator))]
public class ImprovementMenu : MonoBehaviour
{
    private const int MaxCountImprovementButtons = 2;

    [SerializeField] private ImprovementChoiseButton[] _improvementChoiseButtons;
    [SerializeField] private Button _updateButton;
    [SerializeField] private AddCoinsButton _addCoinsButton;

    private IMenu _previousMenu;
    private IGamePayTransaction _selectTransaction;
    private IImprovementShop _improvementShop;
    private IAdsViewer _adsViewer;
    private IUIAnimator _animator;

    private void OnValidate()
    {
        if (_improvementChoiseButtons == null || _improvementChoiseButtons.Length != MaxCountImprovementButtons)
            _improvementChoiseButtons = new ImprovementChoiseButton[MaxCountImprovementButtons];

        foreach (var button in _improvementChoiseButtons)
            if (button == null)
                throw new NullReferenceException(nameof(button));

        if (_updateButton == null)
            throw new NullReferenceException(nameof(_updateButton));

        if (_addCoinsButton == null)
            throw new NullReferenceException(nameof(_addCoinsButton));
    }

    private void Awake()
    {
        _animator = GetComponent<MenuAnimator>();

        gameObject.SetActive(false);
        _updateButton.interactable = false;
    }

    private void OnEnable()
    {
        foreach (var button in _improvementChoiseButtons)
            button.Clicked += OnSelectButton;

        _addCoinsButton.VideoViewed += OnUpdateMenu;
    }

    private void OnDisable()
    {
        foreach (var button in _improvementChoiseButtons)
            button.Clicked -= OnSelectButton;

        _addCoinsButton.VideoViewed -= OnUpdateMenu;
    }

    public void Initialize(IImprovementShop improvementShop, IAdsViewer adsViewer)
    {
        _improvementShop = improvementShop ?? throw new ArgumentNullException(nameof(improvementShop));
        _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));

        for (int i = 0; i < _improvementChoiseButtons.Length; i++)
            _improvementChoiseButtons[i].SetIndex(i);
    }

    public void Open(IMenu previousMenu)
    {
        _previousMenu = previousMenu ?? throw new ArgumentNullException(nameof(previousMenu));

        _adsViewer.ShowFullScreenAd();
        gameObject.SetActive(true);

        StartCoroutine(WaitOpening());
    }

    public void OnImprove()
    {
        if (_improvementShop.TryMakeTransaction(_selectTransaction))
            foreach (var button in _improvementChoiseButtons)
                button.Enable();
        else
            throw new InvalidOperationException("The transaction failed");

        _selectTransaction = null;
        _updateButton.interactable = false;
    }

    public void OnClose()
    {
        _updateButton.interactable = false;

        StartCoroutine(WaitClosure());
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

    private void OnUpdateMenu()
    {
        foreach (var button in _improvementChoiseButtons)
            button.Enable();
    }

    private IEnumerator WaitOpening()
    {
        yield return _animator.PlayOpen();

        _updateButton.interactable = false;

        foreach (var button in _improvementChoiseButtons)
            button.Enable();
    }

    private IEnumerator WaitClosure()
    {
        yield return _animator.PlayClose();

        gameObject.SetActive(false);
        _previousMenu.Enable();
    }
}