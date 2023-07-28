using System;
using Input;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

namespace UI.Views
{
    public class SensorView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private IPlayerInput _playerInput;

        [Inject]
        private void Construct(IPlayerInput input)
        {
            _playerInput = input;
        }

        private void Start()
        {
#if !UNITY_EDITOR
            InputSystem.EnableDevice(Accelerometer.current);
#endif
        }
        private void Update()
        {
            text.text =
                $"Acceleration\nX={_playerInput.Acceleration.x:#0.00} Y={_playerInput.Acceleration.y:#0.00} Z={_playerInput.Acceleration.z:#0.00}\n\n";
        }
    }
}