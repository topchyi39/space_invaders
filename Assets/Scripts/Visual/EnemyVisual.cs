using System;
using Entities;
using Entities.EnemyEntity;
using ObjectPolling;
using UnityEngine;
using VisualEffects;

namespace Visual
{
    public class EnemyVisual : MonoBehaviour
    {
        [SerializeField] private EntityBoundary boundary;
        [SerializeField] private MeshFilter defaultMeshFilter;
        [SerializeField] private MeshFilter secondMeshFilter;
        [SerializeField] private MeshRenderer meshRenderer;
        
        private Mesh _defaultMesh;
        private Mesh _secondMesh;
        private bool _toggled;
        private Material _material;
        private Color _mainColor = Color.white;

        public EntityBoundary Boundary => boundary;
        
        private void Awake()
        {
            _defaultMesh = defaultMeshFilter.sharedMesh;
            _secondMesh = secondMeshFilter.sharedMesh;
        }

        public void Enable()
        {
            // _toggled = false;
            // defaultMeshFilter.sharedMesh = _toggled ? _secondMesh : _defaultMesh;
            
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
        
        public void ToggleVisual()
        {
            _toggled = !_toggled;
            defaultMeshFilter.sharedMesh = _toggled ? _secondMesh : _defaultMesh;
        }

        public void SetGold(bool state)
        {
            _material ??= meshRenderer.material;
            _mainColor = state ? Color.yellow : Color.white;
            _material.color = _mainColor;
        }

        public void SpawnDestroyEffect(VisualEffectPool visualEffectPool)
        {
            visualEffectPool.SpawnEffect<DestroyingEffect>(transform.position, transform.rotation, _mainColor);
            
        }
    }
}