using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public interface IPlayerInput
    {
        float HorizontalAxis { get; }
        bool Fire { get; }
    }
    
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        private PlayerInputActions _playerInputActions;
        private float _startX;
        private float _halfOfScreenWidth;
        private bool _canUpdateAxis;
        private bool _frameIsSkipped;

        public float HorizontalAxis { get; private set; }
        public bool Fire { get; private set; }

        private void OnEnable()
        {
            _playerInputActions ??= new PlayerInputActions();
            
            _playerInputActions.Enable();
            SubscribeOnEvents();
        }

        private void Start()
        {
            _halfOfScreenWidth = Screen.width / 2f;
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
            UnsubscribeOnEvents();
        }

        private void Update()
        {
            ReadHorizontalAxis();
            ReadFire();
        }

        private void ReadFire()
        {
            if (!_frameIsSkipped)
            {
                _frameIsSkipped = true;
                return;
            }
            
            Fire = false;
        }

        private void ReadHorizontalAxis()
        {
            var axisValue = 0f;
#if UNITY_EDITOR
            _canUpdateAxis = true;
#endif
            axisValue = _canUpdateAxis ? _playerInputActions.Player.Moving.ReadValue<float>() : 0;
#if !UNITY_EDITOR
            if (Mathf.Abs(axisValue) > 0)
            {
                axisValue = Mathf.Clamp(_halfOfScreenWidth - axisValue, -1f, 1f);
            }
#endif
            
            
            HorizontalAxis = axisValue;
        }

        private void SubscribeOnEvents()
        {
            _playerInputActions.Player.MovingTouch.started += MovingOnStarted;
            _playerInputActions.Player.MovingTouch.performed += MovingOnPerformed;
            _playerInputActions.Player.MovingTouch.canceled += MovingOnCanceled;

            _playerInputActions.Player.Fire.performed += FirePerformed;
        }

        private void FirePerformed(InputAction.CallbackContext callbackContext)
        {
            _frameIsSkipped = false;
            Fire = true;
        }
        
        private void MovingOnPerformed(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("Performed Hold");
            _canUpdateAxis = true;
        }

        private void UnsubscribeOnEvents()
        {
            _playerInputActions.Player.MovingTouch.started -= MovingOnStarted;
            _playerInputActions.Player.MovingTouch.performed -= MovingOnPerformed;
            _playerInputActions.Player.MovingTouch.canceled -= MovingOnCanceled;
        }

        private void MovingOnStarted(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("Started Hold");
        }

        private void MovingOnCanceled(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("Canceled Hold");
            _canUpdateAxis = false;
        }
    }
}