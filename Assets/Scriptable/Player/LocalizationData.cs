using System;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Player/LocalizationData", fileName = "LocalizationData", order = 51)]
    public class LocalizationData : ScriptableObject
    {
        private ILocalisatorService _localizationService;

        public event Action LanguageChanged;

        public Language Language => _localizationService.Language;
        public bool IsLanguageEstablished => _localizationService != null;

        public void EstablishLanguage(ILocalisatorService localizationService)
        {
            _localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));

            LanguageChanged?.Invoke();
        }
    }
}
