using UnityEngine;

namespace Codebase.Runtime.Movement
{
    public interface IMovement
    {
        void MoveTo(Vector3 destination, float speed);
        void Stop();
    }
}