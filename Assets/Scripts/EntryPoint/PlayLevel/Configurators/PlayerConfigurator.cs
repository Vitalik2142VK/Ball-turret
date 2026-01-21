using System;
using UnityEngine;
using Scriptable;

namespace PlayLevel
{
    public class PlayerConfigurator : MonoBehaviour
    {
        [SerializeField] private CachedPlayer _cachedUser;
        [SerializeField] private ImprovementTurretAttributes _improvementTurretAttributes;

        private IPlayerLoader _playerLoader;
        private ISavedPlayerData _savedData;
        private IPlayerSaver _playerSaver;
        private CoinAdder _coinAdder;


        private void OnValidate()
        {
            if (_cachedUser == null)
                throw new NullReferenceException(nameof(_cachedUser));

            if (_improvementTurretAttributes == null)
                throw new NullReferenceException(nameof(_improvementTurretAttributes));
        }

        public void OnDisable()
        {
            if (_coinAdder != null)
                _coinAdder.Disable();
        }

        public void Configure(AdsViewer adsViewer)
        {
            if (adsViewer == null)
                throw new ArgumentNullException(nameof(adsViewer));

            _savedData ??= new SavedPlayerData();
            _playerLoader = new PlayerLoader(_improvementTurretAttributes, _savedData);

            if (_cachedUser.IsLoaded == false)
            {
                IPlayer player = _playerLoader.Load();
                _cachedUser.SetPlayer(player);
            }

            _playerSaver = new PlayerSaver(_cachedUser, _savedData);

            adsViewer.Initialize(_cachedUser.PurchasesStorage);

            _coinAdder = new CoinAdder(_playerSaver, _cachedUser.Wallet, adsViewer);
        }
    }
}
