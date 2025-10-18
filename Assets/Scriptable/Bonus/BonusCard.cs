using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Description/Bonus Card", fileName = "BonusCard", order = 51)]
    public class BonusCard : ScriptableObject, IBonusCard
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;

        [Header("Description")]
        [SerializeField] private string _enDescription;
        [SerializeField] private string _ruDescription;
        [SerializeField] private string _trDescription;

        [Header("Optional")]
        [SerializeField] private BonusView _view;

        private Dictionary<Language, string> _descriptions;

        private void OnValidate()
        {
            if (_view != null)
                _name = _view.name;

            if (_icon == null)
                throw new NullReferenceException(nameof(_icon));

            if (_enDescription.Length == 0)
                throw new IndexOutOfRangeException(nameof(_enDescription));

            if (_ruDescription.Length == 0)
                throw new IndexOutOfRangeException(nameof(_ruDescription));

            if (_trDescription.Length == 0)
                throw new IndexOutOfRangeException(nameof(_trDescription));
        }

        public Sprite Icon => _icon;
        public string Name => _name;

        public string GetDescription(Language language)
        {
            _descriptions ??= CreateDescriptions();

            if (_descriptions.ContainsKey(language) == false)
                throw new ArgumentOutOfRangeException(nameof(language));

            return _descriptions[language];
        }

        private Dictionary<Language, string> CreateDescriptions()
        {
            return new Dictionary<Language, string>()
            {
                { Language.EN, _enDescription },
                { Language.RU, _ruDescription },
                { Language.TR, _trDescription },
            };
        }
    }
}
