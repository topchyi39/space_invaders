using System;
using UnityEngine;

namespace CameraTools
{
    public class CameraBoundary : MonoBehaviour
    {
        [SerializeField] private Camera camera;

        public bool CheckPointInBoundary(Vector3 point, Vector3 halfExtends, bool debug = false)
        {
            
            var left = point;
            left.x -= halfExtends.x;
            left.y -= halfExtends.y;
            
            var right = point;
            right.x += halfExtends.x;
            right.y += halfExtends.y;
            
            var result = CheckViewportPositions(left, right);
            
            
            if (debug) Debug.Log(result);
            
            return result;
        }
        
        public bool CheckPointInBoundary(Vector3 point, float halfExtends, bool debug = false)
        {
            
            var left = point;
            left.x -= halfExtends;
            left.y -= halfExtends;
            
            var right = point;
            right.x += halfExtends;
            right.y += halfExtends;
            
            var result = CheckViewportPositions(right, left);

            if (debug) Debug.Log(result);
            
            return result;
        }

        private bool CheckViewportPositions(Vector3 left, Vector3 right)
        {
            var leftViewPortPoint = camera.WorldToViewportPoint(left);
            var rightViewPortPoint = camera.WorldToViewportPoint(right);

            var result = leftViewPortPoint is { x: > 0, y: > 0 } &&
                         rightViewPortPoint is { x: < 1, y: < 1 };
            return result;
        }

        public bool CheckPointInVerticalBoundary(Vector3 point, float pointSize)
        {
            var screenPoint = camera.WorldToScreenPoint(point);
            
            var upCorner = camera.ScreenToWorldPoint(new Vector3(screenPoint.x, camera.pixelHeight, screenPoint.z)).y;
            var downCorner = camera.ScreenToWorldPoint(new Vector3(screenPoint.x, 0, screenPoint.z)).y;
            return  point.y - pointSize > downCorner && point.y + pointSize < upCorner;
        }
        
    }
}