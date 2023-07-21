using UnityEngine;

namespace Entities
{
    public class EntityBoundary : MonoBehaviour
    {
        [SerializeField] private Vector3 bound;

        public Vector3 Bound => bound;
        public Vector3 HalfExtends => bound / 2f;
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, bound);
        }
    }
}