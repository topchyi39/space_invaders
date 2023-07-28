using UnityEngine;

namespace MenuComponents
{
    public class IdleParallaxState : ParallaxState
    {
        private Vector3 _startAcceleration;
        private Vector3 _currentAcceleration;
        private Quaternion _targetRotation;
        private Quaternion _textRotation;
        
        public override void Enter()
        {
            _startAcceleration = Input.Acceleration;
            _targetRotation = Quaternion.identity;
            _textRotation = Quaternion.identity;
            Reset(Target);
            Reset(Text);
        }

        public override void Update()
        {
            base.Update();
            if (_startAcceleration == Vector3.zero) { _startAcceleration = Input.Acceleration; }
            
            
            var tempAcceleration = (Input.Acceleration - _startAcceleration) * 60f;
            _currentAcceleration = new Vector3(-tempAcceleration.y, tempAcceleration.x, 0);
        }

        public override void FixedUpdate()
        {
            Rotate(Target, _currentAcceleration, ref _targetRotation);
            Rotate(Text, _currentAcceleration / 20f, ref _textRotation);
            SkyboxSettings.SetAngleMultiplier(Input.Acceleration.x / 4f);
        }

        private void Rotate(Transform transform, Vector3 acceleration, ref Quaternion rotation)
        {
            var targetQuaternion = Quaternion.Euler(acceleration);
            rotation = Quaternion.Slerp(rotation, targetQuaternion, Time.deltaTime);
            transform.localRotation = rotation;
        }

        private void Reset(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }
    }
}