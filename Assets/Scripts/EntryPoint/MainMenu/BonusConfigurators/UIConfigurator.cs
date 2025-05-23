using System;
using UnityEngine;

namespace MainMenuSpace
{
    public class UIConfigurator : MonoBehaviour
    {
        [SerializeField] private PlaySceneLoader _sceneLoader;
        [SerializeField] private PlayMenu _playMenu;
        [SerializeField] private ImprovementMenu _improvementChoiseMenu;
        [SerializeField] private ImprovementChoiseButton _updateHealthButton;
        [SerializeField] private ImprovementChoiseButton _updateDamageButton;

        private void OnValidate()
        {
            if (_sceneLoader == null)
                throw new NullReferenceException(nameof(_sceneLoader));

            if (_playMenu == null)
                throw new NullReferenceException(nameof(_playMenu));

            if (_improvementChoiseMenu == null)
                throw new NullReferenceException(nameof(_improvementChoiseMenu));

            if (_updateHealthButton == null)
                throw new NullReferenceException(nameof(_updateHealthButton));

            if (_updateDamageButton == null)
                throw new NullReferenceException(nameof(_updateDamageButton));
        }

        public void Configure(IUser user, ILevelFactory levelFactory, IImprovementShop improvementShop)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (levelFactory == null)
                throw new ArgumentNullException(nameof(levelFactory));

            if (improvementShop == null)
                throw new ArgumentNullException(nameof(improvementShop));

            _playMenu.Initialize(user, levelFactory, _sceneLoader);
            _improvementChoiseMenu.Initialize(improvementShop);

            InitializeImprovementChoiseButtons(improvementShop);
        }

        public void InitializeImprovementChoiseButtons(IImprovementShop improvementShop)
        {
            var transaction = improvementShop.GetTransaction(typeof(HealthImprovementTransaction));
            var product = improvementShop.GetProduct(typeof(HealthImprovementProduct));

            _updateHealthButton.Initialize(transaction, product);

            transaction = improvementShop.GetTransaction(typeof(DamageImprovementTransaction));
            product = improvementShop.GetProduct( typeof(DamageImprovementProduct));

            _updateDamageButton.Initialize(transaction, product);
        }
    }
}
