using System;
using MenuComponents;
using UI.Screens;

namespace UI.ViewModels
{
    public class MenuViewModel : IViewModel
    {
        private readonly Menu _menu;
        private MenuParallax _parallax;
        
        public event Action OnDataChanged;

        public MenuViewModel(Menu menu, MenuParallax parallax)
        {
            _menu = menu;
            _parallax = parallax;
        }
        
        public void Play()
        {
            _parallax.ToGameTransition(_menu.PlayGame);
        }

        public void SetParallaxToIdle()
        {
            _parallax.ToIdle();
        }
    }
}