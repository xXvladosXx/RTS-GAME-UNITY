using UnityEngine;

namespace Codebase.Runtime.CameraSystem.Zoom
{
    public interface ICameraZoom
    {
        public void Zoom(Transform transform, float speed);
    }
}