using System;

namespace UI.Screens
{
    public class WalletViewModel : IViewModel
    {
        private int goldCount;

        public WalletViewModel(int goldValue)
        {
            goldCount = goldValue;
        }

        public int GoldCount
        {
            get => goldCount;
            set
            {
                goldCount = value;
                OnDataChanged?.Invoke();
            }
        }

        public event Action OnDataChanged;
    }
}