using CannonTurret.LevelSystem;
using CannonTurret.UI.Animations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CannonTurret.UI.MainMenu
{
    [RequireComponent(typeof(Button), typeof(ScaleButtonAnimator))]
    public class SelectLevelButton : MonoBehaviour
    {
        public const string InfiniteValue = "∞";

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _blockImage;
        [SerializeField] private Image _glowImage;

        private IButtonAnimator _animator;
        private Button _button;

        public event Action<int> Clicked;

        public string TextIndex { get; private set; }

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

            if (_glowImage == null)
                throw new NullReferenceException(nameof(_glowImage));
        }

        private void Awake()
        {
            _animator = GetComponent<IButtonAnimator>();
            _button = GetComponent<Button>();

            _glowImage.gameObject.SetActive(false);
            IsBocked = false;
            Index = -1;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnPress);

            if (IsBocked)
                _animator.PressOut();
            else
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
                TextIndex = InfiniteValue;
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
                colorsButton.disabledColor = colorsButton.normalColor;
                _button.colors = colorsButton;
            }
        }

        public void Select()
        {
            _button.interactable = false;
            _glowImage.gameObject.SetActive(true);
            _animator.PressOut();

            Clicked?.Invoke(Index);
        }

        public void CancelSelection()
        {
            if (IsBocked || _button.interactable)
                return;

            _button.interactable = true;
            _glowImage.gameObject.SetActive(false);
            _animator.Press();
        }

        private void OnPress() => Select();
    }
}