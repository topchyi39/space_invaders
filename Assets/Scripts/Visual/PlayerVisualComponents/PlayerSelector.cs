using System;
using System.Collections;
using UI;
using UI.Screens;
using UnityEngine;
using Visual.PlayerVisual;
using WalletComponents;
using Zenject;

namespace Visual.PlayerVisualComponents
{
    public class PlayerSelector : MonoBehaviour
    {
        [SerializeField] private PlayerModel[] models;

        private int _playerModelIndex;
        private int _buyingPlayerModels;
        
        private BitArray _bitArray;
        
        private UIManager _uiManager;
        private PlayerSelectorViewModel _viewModel;

        private static readonly string PlayerModelIndexKey = nameof(_playerModelIndex);
        private static readonly string BuyingPlayerModelsKey = nameof(_buyingPlayerModels);
        
        public int ModelsCount { get; private set; }
        public PlayerModel CurrentModel => models[_playerModelIndex];
        
        public event Action<PlayerModel> OnModelChanged;

        [Inject]
        private void Construct(UIManager uiManager, IWallet wallet)
        {
            _uiManager = uiManager;
            _viewModel = new PlayerSelectorViewModel(this,wallet);
            Debug.Log(_viewModel);
        }

        private void Start()
        {
            Load();
            ModelsCount = models.Length;
            _viewModel.SetIndex(_playerModelIndex);
            // _viewModel = new PlayerSelectorViewModel(this, _playerModelIndex);
            _uiManager.BindAndShow(_viewModel);
            OnModelChanged?.Invoke(CurrentModel);
        }

        public void SetModel(int index)
        {
            _playerModelIndex = index;
            OnModelChanged?.Invoke(models[_playerModelIndex]);
            Save();
        }
        
        public PlayerModel GetModel(int index)
        {
            return models[index];
        }

        private void Load()
        {
            _playerModelIndex = PlayerPrefs.GetInt(PlayerModelIndexKey, 0);
            _buyingPlayerModels = PlayerPrefs.GetInt(BuyingPlayerModelsKey, 1);
            _bitArray = new BitArray(new[] { _buyingPlayerModels });
        }

        private void Save()
        {
            PlayerPrefs.SetInt(BuyingPlayerModelsKey, GetIntFromBitArray());
            
            if (IsPurchased(_playerModelIndex))
                PlayerPrefs.SetInt(PlayerModelIndexKey, _playerModelIndex);
            
            PlayerPrefs.Save();
        }

        private int GetIntFromBitArray()
        {
            var array = new int[1];
            _bitArray.CopyTo(array, 0);
            return array[0];
        }

        public bool IsPurchased(int index)
        {
            return _bitArray[index];
        }

        public void Purchase(int index)
        {
            _bitArray[index] = true;
            Save();
        }
    }
}