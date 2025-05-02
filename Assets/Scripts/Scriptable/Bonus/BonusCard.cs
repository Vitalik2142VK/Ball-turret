using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Description/Bonus Card", fileName = "BonusCard", order = 51)]
    public class BonusCard : ScriptableObject, IBonusCard
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private string _description;

        private void OnValidate()
        {
            if (_name.Length == 0)
                throw new System.IndexOutOfRangeException(nameof(_name));

            if (_description.Length == 0)
                throw new System.IndexOutOfRangeException(nameof(_description));
        }

        public Sprite Icon => _icon;
        public string Name => _name;
        public string Description => _description;
    }
}
