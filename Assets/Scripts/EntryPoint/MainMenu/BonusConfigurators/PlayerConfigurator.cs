using System;
using UnityEngine;
using Scriptable;
using YG;

namespace MainMenuSpace
{
    public class PlayerConfigurator : MonoBehaviour
    {
        [SerializeField] private CachedPlayer _cachedUser;
        [SerializeField] private ImprovementTurretAttributes _improvementTurretAttributes;
        [SerializeField] private AuthPlayer _authPlayer;
        [SerializeField] private PurchasesStorage _purchasesStorage;

        private IPlayerLoader _playerLoader;
        private ISavesData _savesData;

        public IPlayerSaver PlayerSaver { get; private set; }

        public ITurretImprover TurretImprover => _cachedUser.TurretImprover;
        public IPlayer Player => _cachedUser;

        private void OnValidate()
        {
            if (_cachedUser == null)
                throw new NullReferenceException(nameof(_cachedUser));

            if (_improvementTurretAttributes == null)
                throw new NullReferenceException(nameof(_improvementTurretAttributes));

            if (_authPlayer == null)
                throw new NullReferenceException(nameof(_authPlayer));

            if (_purchasesStorage == null)
                throw new NullReferenceException(nameof(_purchasesStorage));
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += OnCreateSavesData;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= OnCreateSavesData;
        }

        public void Configure()
        {
            OnCreateSavesData();

            _purchasesStorage.LoadPurchases();
            _playerLoader = new PlayerLoader(_improvementTurretAttributes, _savesData, _purchasesStorage);

            if (_cachedUser.IsSaved == false)
            {
                IPlayer player = _playerLoader.Load();
                _cachedUser.SetUser(player);
            }
            else
            {
                _playerLoader.UpdatePurchasesStorage(_cachedUser);
            }

            PlayerSaver = new PlayerSaver(_cachedUser, _savesData);

            _authPlayer.Authorize();
        }

        private void OnCreateSavesData() => _savesData ??= new SavesData();
    }
}
