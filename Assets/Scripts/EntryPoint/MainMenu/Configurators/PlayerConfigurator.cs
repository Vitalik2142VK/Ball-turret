﻿using System;
using UnityEngine;
using Scriptable;

namespace MainMenuSpace
{
    public class PlayerConfigurator : MonoBehaviour
    {
        [SerializeField] private CachedPlayer _cachedUser;
        [SerializeField] private ImprovementTurretAttributes _improvementTurretAttributes;
        [SerializeField] private AuthPlayer _authPlayer;
        [SerializeField] private PurchasesHandler _purchasesHandler;

        private IPlayerLoader _playerLoader;
        private ISavedPlayerData _savedData;

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

            if (_purchasesHandler == null)
                throw new NullReferenceException(nameof(_purchasesHandler));
        }

        public void Configure()
        {
            _savedData ??= new SavedPlayerData();
            _playerLoader = new PlayerLoader(_improvementTurretAttributes, _savedData);

            if (_cachedUser.IsSaved == false)
            {
                IPlayer player = _playerLoader.Load();
                _cachedUser.SetPlayer(player);
            }

            PlayerSaver = new PlayerSaver(_cachedUser, _savedData);

            _authPlayer.Authorize();
            _purchasesHandler.LoadPurchases(_cachedUser.PurchasesStorage);
        }

        //todo Remove on realise
        public void OnRemoveSave()
        {
            if (_savedData is SavedPlayerData data == false)
                return;

            data.RemoveAll();
        }
    }
}
