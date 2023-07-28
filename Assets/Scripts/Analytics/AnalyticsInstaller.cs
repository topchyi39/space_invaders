using UnityEngine;
using Zenject;

namespace Analytics
{
    public class AnalyticsInstaller : MonoInstaller
    {
        [SerializeField] private GameAnalytics analytics;

        public override void InstallBindings()
        {
            Container.Bind<GameAnalytics>().FromInstance(analytics);
        }
    }
}