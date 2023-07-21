using System;
using TMPro;
using UnityEngine;

namespace UI.Views
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text fpsText;
        [SerializeField] private float updateDelay = 0.2f;

        private float _currentDelay;

        private void Update()
        {
            _currentDelay += Time.deltaTime;
            if (_currentDelay >= updateDelay)
            {
                fpsText.text = (1f / Time.deltaTime).ToString("00");
                _currentDelay = 0;
            }
        }
    }
}