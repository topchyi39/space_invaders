using CameraTools;
using Entities;
using Input;
using UnityEngine;
using Zenject;

namespace Moving
{
    public class InputMovingSystem : MonoBehaviour, IMovingSystem
    {
        [SerializeField] private PlayerMovingData movingData;
        [SerializeField] private PlayerRotationData rotationData;

        private CameraBoundary _cameraBoundary;
        private EntityBoundary _entityBoundary;
        private IPlayerInput _playerInput;
        private Transform _transform;
        private Vector3 _rightDirection;
        
        private bool _enabled;

        public Vector3 Position => _transform.position;
        
        [Inject]
        private void Construct(IPlayerInput playerInput, CameraBoundary cameraBoundary)
        {
            _playerInput = playerInput;
            _cameraBoundary = cameraBoundary;
        }

        private void Awake()
        {
            _transform = transform;
            _rightDirection = -_transform.right;
        }

        public void SetEntityBoundary(EntityBoundary boundary)
        {
            _entityBoundary = boundary;
        }

        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
        }

        private void FixedUpdate()
        {
            if (!_enabled) return;
            
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
            var angle = rotationData.AngleRotation * Mathf.Sign(_playerInput.HorizontalAxis);
            return Quaternion.AngleAxis(angle,
                Vector3.up);
        }

        private bool IsAtTheBorder(Vector3 newPosition)
        {
            return _cameraBoundary.CheckPointInHorizontalBoundary(newPosition, _entityBoundary.HalfExtends.x);
        }

        private bool IsHaveInput()
        {
            return Mathf.Abs(_playerInput.HorizontalAxis) > movingData.MovingTolerance;
        }
    }
}