using TMPro;
using UI.Views;
using UnityEngine;

namespace UI.Screens
{
    public class WalletScreen : Screen<WalletViewModel>
    {
        [SerializeField] private TextCounter textCounter;
        
        protected override void ShowCallback()
        {
            base.ShowCallback();
            textCounter.InitializeValue(ViewModel.GoldCount);
        }

        private void UpdateView()
        {
            textCounter.UpdateValue(ViewModel.GoldCount);
        }

        protected override void OnDataChanged()
        {
            UpdateView();
        }
    }
}