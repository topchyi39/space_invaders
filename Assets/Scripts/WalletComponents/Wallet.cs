using System;
using GameComponents;
using UI;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace WalletComponents
{
    [Serializable]
    public struct Gold
    {
        private int _goldCount;
        public int Value => _goldCount;
        private static readonly string GoldCountKey = nameof(_goldCount);

        public void Initialize()
        {
            LoadGold();
        }
        
        public void Add(int goldCount)
        {
            _goldCount += goldCount;
            SaveGold();
        }

        public void Remove(int goldCount)
        {
            _goldCount -= goldCount;
            SaveGold();
        }
       
        private void SaveGold()
        {
            PlayerPrefs.SetInt(GoldCountKey, _goldCount);
            PlayerPrefs.Save();
        }

        private void LoadGold()
        {
            _goldCount = PlayerPrefs.GetInt(GoldCountKey, 0);
        }

        public bool CanRemove(int price)
        {
            return price <= _goldCount;
        }
    }

    public interface IWalletCollectable
    {
        void Add(int amount);
        void MultiplyGoldDuringGame(float multiplier);
    }

    public interface IWallet
    {
        bool TryBuy(int price);
    }
    
    public class Wallet : MonoBehaviour, IWalletCollectable, IWallet, IGameStartListener
    {
        private Gold _gold;
        private int _goldEarnedDuringGame;

        private UIManager _uiManager;
        private WalletViewModel _viewModel;

        [Inject]
        private void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        public void Start()
        {
            _gold.Initialize();
            _viewModel = new WalletViewModel(_gold.Value);
            _uiManager.BindAndShow(_viewModel);
        }

        public void Add(int goldCount)
        {
            _gold.Add(goldCount);
            _goldEarnedDuringGame += goldCount;
            _viewModel.GoldCount = _gold.Value;
        }

        public void MultiplyGoldDuringGame(float multiplier)
        {
            _gold.Add((int)(_goldEarnedDuringGame * multiplier));
        }

        public bool TryBuy(int price)
        {
            if (!_gold.CanRemove(price)) return false;
            
            _gold.Remove(price);
            _viewModel.GoldCount = _gold.Value;
            return true;
        }

        public void OnGameStarted()
        {
            _goldEarnedDuringGame = 0;
        }
    }
}