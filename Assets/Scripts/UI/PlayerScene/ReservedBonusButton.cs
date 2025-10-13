using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReservedBonusButton : MonoBehaviour
{
    [SerializeField] private Scriptable.LocalizationData _localizationData;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Button _infoButton;

    private void OnValidate()
    {
        if (_localizationData == null)
            throw new NullReferenceException(nameof(_localizationData));

        if (_image == null)
            throw new NullReferenceException(nameof(_image));

        if (_description == null)
            throw new NullReferenceException(nameof(_description));

        if (_infoButton == null)
            throw new NullReferenceException(nameof(_infoButton));
    }

    private void OnEnable()
    {
        _infoButton.onClick.AddListener(OnChangeImageDiscripcion);
    }

    private void OnDisable()
    {
        _infoButton.onClick.RemoveListener(OnChangeImageDiscripcion);
    }

    public void Initialize(IBonusCard bonusCard)
    {
        if (bonusCard == null)
            throw new ArgumentNullException(nameof(bonusCard));

        _image.sprite = bonusCard.Icon;
        _description.text = bonusCard.GetDescription(_localizationData.Language);

        SetActiveDiscription(false);
    }

    private void OnChangeImageDiscripcion()
    {
        SetActiveDiscription(_image.gameObject.activeSelf);
    }

    private void SetActiveDiscription(bool isActive)
    {
        _description.gameObject.SetActive(isActive);
        _image.gameObject.SetActive(isActive == false);
    }
}