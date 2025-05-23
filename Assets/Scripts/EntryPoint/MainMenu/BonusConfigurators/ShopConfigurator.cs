﻿using Scriptable;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenuSpace
{
    public class ShopConfigurator : MonoBehaviour
    {
        [SerializeField] private InitialPrices _initialPrices;
        [SerializeField] private TurretAttributes _turretAttributes;
        [SerializeField] private WalletView _walletView;

        private IWallet _wallet;
        private ITurretImprover _turretImprover;

        private void OnValidate()
        {
            if (_initialPrices == null)
                throw new NullReferenceException(nameof(_initialPrices));

            if (_turretAttributes == null)
                throw new NullReferenceException(nameof(_turretAttributes));

            if (_walletView == null)
                throw new NullReferenceException(nameof(_walletView));
        }

        public IImprovementShop ImprovementShop { get; private set; }

        public void Configure(IUser user, ITurretImprover turretImprover)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _wallet = user.Wallet;
            _wallet.SetView(_walletView);
            _turretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));

            var transactions = CreateTransactions();
            var products = CreateProducts();
            ImprovementShop = new ImprovementShop(_wallet, transactions, products);
        }

        private List<IGamePayTransaction> CreateTransactions()
        {
            float magnificationFactor = 1.3f;
            PriceEnlarger damagePriceEnlarger = new PriceEnlarger(_initialPrices.DamageImprovement, magnificationFactor);

            magnificationFactor = 1.9f;
            PriceEnlarger healthPriceEnlarger = new PriceEnlarger(_initialPrices.HealthImprovement, magnificationFactor);

            return new List<IGamePayTransaction>
            {
                new DamageImprovementTransaction(_wallet, _turretImprover, damagePriceEnlarger),
                new HealthImprovementTransaction(_wallet, _turretImprover, healthPriceEnlarger)
            };
        }

        private List<IImprovementProduct> CreateProducts()
        {
            return new List<IImprovementProduct>
            {
                new DamageImprovementProduct(_turretImprover, _turretAttributes.Damage),
                new HealthImprovementProduct(_turretImprover, _turretAttributes.MaxHealth)
            };
        }
    }
}
