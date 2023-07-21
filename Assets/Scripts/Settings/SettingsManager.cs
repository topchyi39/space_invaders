using System;
using System.Collections.Generic;
using Settings.Sound;
using UI;
using UI.ViewModels;
using UnityEngine;
using Zenject;

namespace Settings
{
    [Serializable]
    public abstract class SettingsModule
    {
        public abstract void Init();
    }
    
    public class SettingsManager : MonoBehaviour
    {
        private UIManager _uiManager;
        private SettingsViewModel _settingsViewModel;

        private SoundSettingsModule _module;
        private Dictionary<Type, SettingsModule> _modules = new();

        [Inject]
        private void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        private void Start()
        {
            foreach (var module in _modules.Values)
            {
                module.Init();
            }
            
            _settingsViewModel = new SettingsViewModel(this);
            _uiManager.Bind(_settingsViewModel);
        }

        public void AddModule(SettingsModule module)
        {
            var moduleType = module.GetType();
            if (_modules.ContainsKey(moduleType)) return;
            
            _modules.Add(moduleType, module);
            _module = module as SoundSettingsModule;
        }

        public TModule GetModule<TModule>() where TModule : SettingsModule
        {
            if (_modules.TryGetValue(typeof(TModule), out var module))
                return module as TModule;

            return null;
        }
    }
}