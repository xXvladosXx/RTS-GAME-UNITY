using UnityEngine;

namespace Codebase.Runtime.CameraSystem.Factory
{
    public interface ICameraFacade
    {
        ICameraMain CameraMain { get; }
        public RaycastHit FireRay(Vector3 position, LayerMask layerMask);
        RaycastHit FireRay(Vector3 mousePosition);
        void CreateCameraMain();
    }
}