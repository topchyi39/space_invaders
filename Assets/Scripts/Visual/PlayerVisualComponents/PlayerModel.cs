using Entities;
using UnityEngine;

namespace Visual.PlayerVisual
{
    [CreateAssetMenu(fileName = "PlayerModel", menuName = "RSR/PlayerModel", order = 0)]
    public class PlayerModel : ScriptableObject
    {
        [SerializeField] private Mesh playerMesh;
        [SerializeField] private Material[] materials;
        [SerializeField] private float scale;
        [SerializeField] private Vector3 bounds;
        [SerializeField] private Vector3 centre;
        [SerializeField] private int price;

        public int Price => price;
        
        public void SetMesh(MeshFilter meshFilter, MeshRenderer meshRenderer)
        {
            meshFilter.mesh = playerMesh;
            meshRenderer.materials = materials;
            meshRenderer.transform.localScale = Vector3.one * scale;
        }

        public void SetBounds(BoxCollider collider, EntityBoundary boundary)
        {
            collider.center = centre;
            collider.size = bounds;
            boundary.SetBound(bounds);
        }
    }
}