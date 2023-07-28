using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Visual.PlayerVisualComponents;
using WalletComponents;
using Zenject;

namespace UI.Screens
{
    public class PlayerSelectorViewModel : IViewModel
    {
        private PlayerSelector _selector;
        private IWallet _wallet;
        
        private int _currentIndex;

        public bool CanMoveToPrevious => _currentIndex > 0;
        public bool CanMoveToNext => _currentIndex < _selector.ModelsCount - 1;
        public bool CanBuy => !_selector.IsPurchased(_currentIndex);
        public int CurrentModelPrice => _selector.CurrentModel.Price;

        public PlayerSelectorViewModel(PlayerSelector selector, IWallet wallet)
        {
            _selector = selector;
            _wallet = wallet;
        }

        public void SetIndex(int index)
        {
            _currentIndex = index;
        }

        public void Previous()
        {
            _currentIndex--;
            _selector.SetModel(_currentIndex);
        }

        public void Next()
        {
            _currentIndex++;
            _selector.SetModel(_currentIndex);
        }
        
        public event Action OnDataChanged;

        public void TryPurchase()
        {
            if(_wallet.TryBuy(_selector.CurrentModel.Price))
                _selector.Purchase(_currentIndex);
        }
    }
    
    public class PlayerSelectorScreen : Screen<PlayerSelectorViewModel>
    {
        [SerializeField] private Button toPrevious;
        [SerializeField] private Button toNext;
        [SerializeField] private Button buyButton;
        [SerializeField] private TMP_Text purchase;
        
        private void Awake()
        {
            toPrevious.onClick.AddListener(Previous);
            toNext.onClick.AddListener(Next);
            buyButton.onClick.AddListener(Purchase);
        }

        protected override void ShowCallback()
        {
            ValidateViews();
        }

        protected override void BindCallback(PlayerSelectorViewModel viewModel)
        {
            ValidateViews();
        }

        private void Previous()
        {
            ViewModel.Previous();
            ValidateViews();
        }

        private void Next()
        {
            ViewModel.Next();
            ValidateViews();
        }

        private void Purchase()
        {
            ViewModel.TryPurchase();
            ValidateViews();
        }

        private void ValidateViews()
        {
            toPrevious.gameObject.SetActive(ViewModel.CanMoveToPrevious);
            toNext.gameObject.SetActive(ViewModel.CanMoveToNext);
            buyButton.gameObject.SetActive(ViewModel.CanBuy);
            purchase.text = ViewModel.CurrentModelPrice.ToString();
        }
    }
}