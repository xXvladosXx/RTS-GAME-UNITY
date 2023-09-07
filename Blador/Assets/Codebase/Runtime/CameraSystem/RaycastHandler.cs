using Codebase.Runtime.CameraSystem.Factory;
using UnityEngine;

namespace Codebase.Runtime.CameraSystem
{
    public class RaycastHandler : IRaycastHandler
    {
        public RaycastHit FireRay(Vector3 position, LayerMask layerMask, Camera camera)
        {
            Physics.Raycast(
                camera.ScreenPointToRay(position),
                out var hit,
                camera.farClipPlane,
                layerMask
            );

            return hit;
        }

        public RaycastHit FireRay(Vector3 mousePosition, Camera camera)
        {
            Physics.Raycast(
                camera.ScreenPointToRay(mousePosition),
                out var hit,
                camera.farClipPlane
            );
            
            return hit;
        }
    }
}