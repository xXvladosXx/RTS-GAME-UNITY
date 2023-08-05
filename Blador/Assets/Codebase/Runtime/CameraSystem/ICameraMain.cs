using Codebase.Runtime.GameplayCore;
using UnityEngine;

namespace Codebase.Runtime.CameraSystem
{
    public interface ICameraMain : IGameLoop
    {
        Camera Camera { get; }
        void SwitchMovement();
    }
}