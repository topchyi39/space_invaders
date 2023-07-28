using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

namespace Input
{
    public interface IPlayerInput
    {
        float HorizontalAxis { get; }
        bool Fire { get; }
        Vector3 Acceleration { get; }
    }
    
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        private PlayerInputActions _playerInputActions;
        private float _startX;
        private float _halfOfScreenWidth;
        private float _halfOfScreenHeight;
        private bool _canUpdateAxis;
        private bool _frameIsSkipped;

        public float HorizontalAxis { get; private set; }
        public bool Fire { get; private set; }
        public Vector3 Acceleration { get; private set; }
        private void OnEnable()
        {
            _playerInputActions ??= new PlayerInputActions();
            
            _playerInputActions.Enable();
            SubscribeOnEvents();
        }

        private void Start()
        {
            _halfOfScreenWidth = Screen.width / 2f;
            _halfOfScreenHeight = Screen.height / 2f;
            
#if !UNITY_EDITOR
            InputSystem.EnableDevice(Gyroscope.current);
            InputSystem.EnableDevice(Accelerometer.current);
            Acceleration = Accelerometer.current.acceleration.ReadValue();

            InputSystem.EnableDevice(AttitudeSensor.current);
            InputSystem.EnableDevice(GravitySensor.current);
#endif      
            
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
            ReadSensors();
        }

        private void ReadSensors()
        {
            
#if !UNITY_EDITOR
            Acceleration = Accelerometer.current.acceleration.ReadValue();
#else
            var mousePosition = _playerInputActions.Player.MousePosition.ReadValue<Vector2>();
            var x = Mathf.InverseLerp(-_halfOfScreenWidth ,_halfOfScreenWidth, mousePosition.x - _halfOfScreenWidth);
            var y = Mathf.InverseLerp(-_halfOfScreenHeight ,_halfOfScreenHeight, mousePosition.y - _halfOfScreenHeight);
            var newAcceleration = new Vector3(x, y, 0);
            Acceleration = newAcceleration;
#endif
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
            
            
            HorizontalAxis = -axisValue;
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