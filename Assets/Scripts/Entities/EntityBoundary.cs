using UnityEngine;

namespace Entities
{
    public class EntityBoundary : MonoBehaviour
    {
        [SerializeField] private Vector3 bound;

        public Vector3 Bound => bound;
        public Vector3 HalfExtends => bound / 2f;

        public void SetBound(Vector3 newBound)
        {
            if (newBound == Vector3.zero) return;

            bound = newBound;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, bound);
        }
    }
}