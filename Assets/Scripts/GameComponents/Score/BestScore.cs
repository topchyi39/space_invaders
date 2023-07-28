using UnityEngine;

namespace GameComponents.Score
{
    public struct BestScore
    {
        private int? _bestScore;

        private static string BestScoreKey => nameof(_bestScore);
        
        public bool CheckForBest(int score)
        {
            if(!_bestScore.HasValue) GetBestScore();
            
            if (score <= _bestScore) return false;
            
            SetNewBestScore(score);
            return true;
        }

        private void GetBestScore()
        {
            _bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        }

        private void SetNewBestScore(int score)
        {
            _bestScore = score;
            PlayerPrefs.SetInt(BestScoreKey, _bestScore.Value);
            PlayerPrefs.Save();
        }
    }
}