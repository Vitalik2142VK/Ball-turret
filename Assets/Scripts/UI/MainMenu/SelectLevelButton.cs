using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(ScaleButtonAnimator))]
public class SelectLevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _blockImage;

    private IButtonAnimator _animator;
    private Button _button;
    private RectTransform _rectTransform;

    public event Action<int> Clicked;

    public string TextIndex {  get; private set; }
    public int Index { get; private set; }
    public bool IsBocked { get; private set; }

    private void OnValidate()
    {
        if (_text == null)
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();

            if (_text == null)
                throw new NullReferenceException(nameof(_text));
        }

        if (_blockImage == null)
            throw new NullReferenceException(nameof(_blockImage));
    }

    private void Awake()
    {
        _animator = GetComponent<IButtonAnimator>();
        _button = GetComponent<Button>();

        _rectTransform = GetComponent<RectTransform>();
        Index = -1;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnPress);

        if (IsBocked == false)
            _animator.Press();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnPress);   
    }

    public void SetIndex(int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        Index = index;

        if (index == EndlessLevel.IndexLevel)
            TextIndex = "∞";
        else
            TextIndex = index.ToString();

        _text.text = TextIndex;
    }

    public void SetBlock(bool isBlock)
    {
        IsBocked = isBlock;

        _blockImage.gameObject.SetActive(isBlock);
        _button.interactable = !isBlock;

        if (isBlock)
        {
            var colorsButton = _button.colors;
            colorsButton.normalColor = colorsButton.disabledColor;
        }
    }

    public void SetSize(float scale)
    {
        if (scale < 0)
            throw new ArgumentOutOfRangeException(nameof(scale));

        var rect = _rectTransform.rect;

        Debug.Log($"sizes == {rect.size}");

        _rectTransform.sizeDelta *= scale;
    }

    public void Select()
    {
        _button.interactable = false;
        _animator.Press();

        Clicked?.Invoke(Index);
    }

    public void CancelSelection()
    {
        _button.interactable = true;
        _animator.PressOut();
    }

    private void OnPress() => Select();
}
