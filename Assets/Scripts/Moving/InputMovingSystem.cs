using Input;
using UnityEngine;
using Zenject;

namespace Moving
{
    public class InputMovingSystem : MonoBehaviour, IMovingSystem
    {
        [SerializeField] private PlayerMovingData movingData;
        [SerializeField] private PlayerRotationData rotationData;
        
        private IPlayerInput _playerInput;
        private Transform _transform;
        private Vector3 _rightDirection;
        
        private bool _canMove;
        
        [Inject]
        private void Construct(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        private void Awake()
        {
            _transform = transform;
            _rightDirection = _transform.right;
        }

        public void Enable()
        {
            _canMove = true;
        }

        public void Disable()
        {
            _canMove = false;
        }

        private void FixedUpdate()
        {
            Move(out var newPosition);
            Rotate(newPosition);
        }

        private void Move(out Vector3 newPosition)
        {
            newPosition = _transform.position;
            
            if(!IsHaveInput()) return;
            
            newPosition = _transform.position + GetVelocity();
            
            if(IsAtTheBorder(newPosition)) return;
            
            _transform.position = newPosition;
        }

        private void Rotate(Vector3 position)
        {
            var rotation = Quaternion.identity;
            
            if (IsHaveInput() && !IsAtTheBorder(position))
            {
                rotation = GetRotation();
                
            }
            
            _transform.localRotation = Quaternion.Slerp(_transform.localRotation, rotation, Time.deltaTime *  rotationData.RotationSpeed);
        }

        private Vector3 GetVelocity()
        {
            return _rightDirection * (_playerInput.HorizontalAxis * Time.deltaTime * movingData.MovingSpeed);
        }

        private Quaternion GetRotation()
        {
            return Quaternion.AngleAxis(rotationData.AngleRotation * Mathf.Sign(_playerInput.HorizontalAxis),
                Vector3.up);
        }

        private bool IsAtTheBorder(Vector3 newPosition)
        {
            return movingData.BorderValue - Mathf.Abs(newPosition.x) < movingData.BorderTolerance;
        }

        private bool IsHaveInput()
        {
            return Mathf.Abs(_playerInput.HorizontalAxis) > movingData.MovingTolerance;
        }
    }
}