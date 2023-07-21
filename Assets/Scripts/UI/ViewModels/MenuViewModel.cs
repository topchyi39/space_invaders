using System;
using GameComponents.Menu;
using UI.Screens;

namespace UI.ViewModels
{
    public class MenuViewModel : IViewModel
    {
        private Menu _menu;
        
        public event Action OnDataChanged;

        public MenuViewModel(Menu menu)
        {
            _menu = menu;
        }
        
        public void Play()
        {
            _menu.PlayGame();
        }

    }
}