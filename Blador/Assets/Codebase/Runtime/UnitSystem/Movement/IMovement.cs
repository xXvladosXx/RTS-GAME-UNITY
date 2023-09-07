using UnityEngine;

namespace Codebase.Runtime.Movement
{
    public interface IMovement
    {
        bool IsStopped { get; }
        float RemainingDistance { get; }
        void MoveTo(Vector3 destination, float speed);
        void Stop();
        void MoveTo(Vector3 destination);
    }
}