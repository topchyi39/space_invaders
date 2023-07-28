using System;
using Combat;
using Combat.Projectiles;
using Entities;
using Moving;
using UnityEngine;

namespace Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private BuildingPart prefab;
        [SerializeField] private int width;
        [SerializeField] private int height;
        
        [SerializeField, HideInInspector] private BuildingPart[] parts;
        
        private Projectile _projectile;
        private Vector3 _projectileBounds;
        
        public EntityBoundary EntityBoundary { get; }
        public IMovingSystem MovingSystem { get; }
        public ICombatSystem CombatSystem { get; }
        
        
        [ContextMenu("ConstructBuilding")]
        private void ConstructBuilding()
        {
            foreach (var goTransform in parts)
            {
                if(goTransform)
                    DestroyImmediate(goTransform.gameObject);
            }
            
            parts = new BuildingPart[width * height];
            
            var scale = prefab.transform.localScale.x;
            var offset = scale / 2;
            var halfWidth = (width / 2f) * scale - offset;
            
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    var goTransform = Instantiate(prefab, transform);
                    goTransform.transform.localPosition = new Vector3(halfWidth - i * scale , j * scale + offset, 0);
                    parts[i * height + j] = (goTransform);
                }
            }
        }

        private void Awake()
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    var part = parts[i * height + j];

                        part.SetNeighbour(Side.Right, i + 1 < width ? parts[(i + 1) * height + j] : null);
                        part.SetNeighbour(Side.Left, i - 1 >= 0 ? parts[(i - 1) * height + j] : null);
                        part.SetNeighbour(Side.Up, j + 1 < height ? parts[i * height + (j + 1)] : null);
                        part.SetNeighbour(Side.Down, j - 1 >= 0 ? parts[i * height + (j - 1)] : null);
                }
            }
        }

        public void Reset()
        {
            foreach (var part in parts)
            {
                part.Reset();
            }
        }

        public void EnableBuilding()
        {
            gameObject.SetActive(true);
        }

        public void DisableBuilding()
        {
            gameObject.SetActive(false);
        }
    }
}