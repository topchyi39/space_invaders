using UnityEngine;

namespace Skybox
{
    public class SkyboxSettings : MonoBehaviour
    {
        private Material _skybox;
        private float _currentRotation;
        private float _targetRotation;
        private float _multiplier;

        private const float DefaultRotation = 270f;
        
        private static readonly int Rotation = Shader.PropertyToID("_Rotation");

        private void Awake()
        {
            _skybox = RenderSettings.skybox;
            _skybox.SetFloat(Rotation, DefaultRotation);
            _currentRotation = DefaultRotation;
        }

        public void SetAngleMultiplier(float multiplier)
        {
            _multiplier = Mathf.Clamp(multiplier, -1f, 1f);
            _targetRotation = DefaultRotation + DefaultRotation * _multiplier;
        }
        
        private void Update()
        {
            _currentRotation = Mathf.Lerp(_currentRotation, _targetRotation, Time.deltaTime);
            _skybox.SetFloat(Rotation, _currentRotation);
        }
    }
}