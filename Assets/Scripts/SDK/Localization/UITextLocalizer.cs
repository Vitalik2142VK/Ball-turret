using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UITextLocalizer : MonoBehaviour
{
    [SerializeField] private Scriptable.LocalizationData _localizationData;
    [SerializeField] private string _englishText;
    [SerializeField] private string _russianText;
    [SerializeField] private string _turkishText;

    private TextMeshProUGUI _text;
    private Language _language;

    private void OnValidate()
    {
        if (_localizationData == null)
            throw new System.NullReferenceException(nameof(_localizationData));

        if (_englishText == null || _englishText.Length == 0)
            _englishText = "Text";

        if (_russianText == null || _russianText.Length == 0)
            _russianText = "Текст";

        if (_turkishText == null || _turkishText.Length == 0)
            _turkishText = "Metin";
    }

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = _englishText; 
    }

    private void OnEnable()
    {
        _localizationData.LanguageChanged += OnChangeLanguage;
    }

    private void Start()
    {
        OnChangeLanguage();
    }

    private void OnDisable()
    {
        _localizationData.LanguageChanged -= OnChangeLanguage;
    }

    private void OnChangeLanguage()
    {
        if (_localizationData.Language == _language)
            return;
        else
            _language = _localizationData.Language;

        switch (_language)
        {
            case Language.EN:
                _text.text = _englishText;
                break;

            case Language.RU:
                _text.text = _russianText;
                break;

            case Language.TR:
                _text.text = _turkishText;
                break;

            default:
                _text.text = _englishText;
                break;
        }
    } 
}

