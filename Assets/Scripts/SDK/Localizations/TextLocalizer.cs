using UnityEngine;

public class TextLocalizer : MonoBehaviour
{
    [SerializeField] private Scriptable.LocalizationData _localizationData;
    [SerializeField, TextArea] private string _englishText;
    [SerializeField, TextArea] private string _russianText;
    [SerializeField, TextArea] private string _turkishText;

    public string Text => GetText();

    private void OnValidate()
    {
        if (_englishText == null || _englishText.Length == 0)
            _englishText = "Text";

        if (_russianText == null || _russianText.Length == 0)
            _russianText = "Текст";

        if (_turkishText == null || _turkishText.Length == 0)
            _turkishText = "Metin";
    }

    private string GetText()
    {
        switch (_localizationData.Language)
        {
            case Language.EN:
                return _englishText;

            case Language.RU:
                return _russianText;

            case Language.TR:
                return _turkishText;

            default:
                return _englishText;
        }
    }
}
