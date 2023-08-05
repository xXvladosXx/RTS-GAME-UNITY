using UnityEngine;

namespace Codebase.Runtime.CameraSystem.Rotation
{
    public interface ICameraRotation
    {
        void Rotate(Transform transform, float speed);
    }
}