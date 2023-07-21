using TMPro;
using UI.ViewModels;
using UnityEngine;

namespace UI.Screens.GameScreens
{
    public class ScoreScreen : Screen<ScoreViewModel>
    {
        [SerializeField] private TMP_Text scoreText;

        protected override void BindCallback(ScoreViewModel viewModel)
        {
            OnDataChanged();
        }

        protected override void OnDataChanged()
        {
            scoreText.text = ViewModel.ScorePoints.ToString();
        }
    }
}