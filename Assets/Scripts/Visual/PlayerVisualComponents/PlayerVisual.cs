using Entities;
using UnityEngine;
using Visual.PlayerVisual;
using Zenject;

namespace Visual.PlayerVisualComponents
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;
        
        [SerializeField] private bool changeBoundary;
        [SerializeField] private BoxCollider collider;
        [SerializeField] private EntityBoundary boundary;
        
        public void ChangeModel(PlayerModel model)
        {
            model.SetMesh(meshFilter, meshRenderer);
            
            if(changeBoundary)
                model.SetBounds(collider, boundary);
        }
    }
}