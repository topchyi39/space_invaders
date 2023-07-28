using System;
using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace Analytics
{
    public class GameAnalytics : MonoBehaviour
    {
        private bool _initialized;
        private bool _canUseAnalytics;
        
        private void Start()
        {
            FirebaseApp.CheckDependenciesAsync().ContinueWith(task =>
            {
                var dependency = task.Result;
                if (dependency == DependencyStatus.Available)
                {
                    _canUseAnalytics = true;
                }
                else
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependency}");
                }

                _initialized = true;
                
            });

        }

        public void LogLevelTime(string time)
        {
            if (!_canUseAnalytics)
                return;
            
            FirebaseAnalytics.LogEvent("level_time", "Level_time_in_minutes", time);
        }

        public void LogAdInterstitial()
        {
            if (!_canUseAnalytics)
                return;
            
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression, new Parameter("Ad_Type", "Interstitial_Ad"));
        }

        public void LogAdRewardInterstitial()
        {
            if (!_canUseAnalytics)
                return;
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression, new Parameter("Ad_Type", "Rewarded_Ad"));
        }
    }
}