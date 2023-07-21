using System;
using UI.Screens;

namespace UI.ViewModels
{
    public class ScoreViewModel : IViewModel
    {
        private int _scorePoints;

        public int ScorePoints => _scorePoints;
        
        public event Action OnDataChanged;

        
        public void UpdateScorePoints(int scorePoints)
        {
            if(_scorePoints == scorePoints) return;

            _scorePoints = scorePoints;
            OnDataChanged?.Invoke();
        }
        
    }
}