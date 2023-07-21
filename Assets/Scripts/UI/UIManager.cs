using System;
using System.Collections.Generic;
using System.Linq;
using UI.Screens;
using UI.Screens.SubScreens;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        private List<BaseScreen> _screens = new();
        
        private Dictionary<Type, BaseScreen> _screensMap;
        private Dictionary<Type, BaseScreen> _subScreensMap;
        private Dictionary<Type, BaseScreen> _shownScreens = new ();
        
        private void Awake()
        {
            InitializeScreens();
        }

        private void InitializeScreens()
        {
            _screensMap = _screens.ToDictionary(screen => screen.ViewModelType, screen => screen);
        }

        public void AddScreen(BaseScreen screen)
        {
            if(_screens.Contains(screen)) return;
            
            _screens.Add(screen);
        }

        public TScreen GetScreen<TScreen>() where TScreen : BaseScreen
        {
            var type = typeof(TScreen);
            return _screens.Find(screen => screen.GetType() == type) as TScreen;
        }

        public void BindAndShow<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            Bind(viewModel);
            Show<TViewModel>();
        }

        public void Bind<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            var viewModelType = typeof(TViewModel);
            if (!_screensMap.TryGetValue(viewModelType, out var screen)) return;
            
            screen.Bind(viewModel as TViewModel);
           
        }

        public void Show<TViewModel>()
        {
            var viewModelType = typeof(TViewModel);
            if (!_screensMap.TryGetValue(viewModelType, out var screen)) return;
            screen.Show();
            _shownScreens.Add(viewModelType, screen);
        }

        public void Hide<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            var viewModelType = typeof(TViewModel);
            if (!_screensMap.TryGetValue(viewModelType, out var screen)) return;
            
            screen.Hide();
            _shownScreens.Remove(viewModelType);
        }
    }
}