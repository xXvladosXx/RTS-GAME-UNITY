using UnityEngine;

namespace Codebase.Runtime.CameraSystem.Movement
{
    public interface ICameraMovement
    {
        void Move(Transform transform, float speed);
    }
}