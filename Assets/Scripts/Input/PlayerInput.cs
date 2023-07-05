using System;
using UnityEngine;

namespace Input
{
    public interface IPlayerInput
    {
        float HorizontalAxis { get; }
    }
    
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        private PlayerInputActions _playerInputActions;
        
        public float HorizontalAxis { get; private set; }

        private const float MAX_DELTA = 10f;
        
        private void OnEnable()
        {
            _playerInputActions ??= new PlayerInputActions();
            _playerInputActions.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
        }

        private void Update()
        {
            var axisValue = _playerInputActions.Player.Moving.ReadValue<float>();

            if (Mathf.Abs(axisValue) > 1)
            {
                axisValue = Mathf.Clamp(axisValue, -MAX_DELTA, MAX_DELTA);
                axisValue /= MAX_DELTA;
            }

            HorizontalAxis = -axisValue;
        }
    }
}