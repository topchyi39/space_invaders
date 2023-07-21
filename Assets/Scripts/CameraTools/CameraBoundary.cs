using System;
using UnityEngine;

namespace CameraTools
{
    public class CameraBoundary : MonoBehaviour
    {
        [SerializeField] private Camera camera;

        public bool CheckPointInHorizontalBoundary(Vector3 point, float pointSize)
        {
            var screenPoint = camera.WorldToScreenPoint(point);
            
            var leftCorner = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, screenPoint.y, screenPoint.z)).x;
            var rightCorner = camera.ScreenToWorldPoint(new Vector3(0, screenPoint.y, screenPoint.z)).x;

            return !(point.x - pointSize > rightCorner && point.x  + pointSize < leftCorner);
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