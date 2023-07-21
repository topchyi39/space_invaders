using System;
using Entities;
using Entities.EnemyEntity;
using UnityEngine;

namespace Visual
{
    public class EnemyVisual : MonoBehaviour
    {
        [SerializeField] private EntityBoundary boundary;
        [SerializeField] private MeshFilter defaultMeshFilter;
        [SerializeField] private MeshFilter secondMeshFilter;

        private Mesh _defaultMesh;
        private Mesh _secondMesh;
        private bool _toggled;

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
    }
}