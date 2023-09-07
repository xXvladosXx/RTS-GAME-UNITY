using UnityEngine;

namespace Codebase.Runtime.CameraSystem
{
    public interface IRaycastHandler
    {
        RaycastHit FireRay(Vector3 position, LayerMask layerMask, Camera camera);
        RaycastHit FireRay(Vector3 mousePosition, Camera camera);
    }
}