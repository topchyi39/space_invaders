using Settings.Sound;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Settings
{
    public class SettingsInstaller : MonoInstaller
    {
        [SerializeField] private SettingsManager settingsManager;
        [SerializeField] private SoundSettingsReferences soundSettingsReferences;
        
        
        
        public override void InstallBindings()
        {
            Container.Bind<SettingsManager>().FromInstance(settingsManager);
            Container.Bind<SoundSettingsReferences>().FromInstance(soundSettingsReferences);
            Container.Bind<SoundSettingsModule>().AsSingle().NonLazy();
        }
    }
}