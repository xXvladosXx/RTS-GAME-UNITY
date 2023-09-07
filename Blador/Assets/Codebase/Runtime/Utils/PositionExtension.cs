using UnityEngine;

namespace Codebase.Runtime.Utils
{
    public static class PositionExtension
    {
        public static float FlatDistanceTo(this Vector3 first, Vector3 second)
        {
            var a = new Vector2(first.x, first.z);
            var b = new Vector2(second.x, second.z);

            return Vector2.Distance(a, b);
        }

        public static Vector3 FlatMoveTowardsTo(this Vector3 start, Vector3 target, float speed)
        {
            var targetYAligned = new Vector3(target.x, start.y, target.z);

            return Vector3.MoveTowards(start, targetYAligned, speed);
        }

        public static Quaternion FlatRotateTowardsTo(this Quaternion rotation, Vector3 target, float speed)
        {
            target.y = 0;

            if (target == Vector3.zero)
                return rotation;

            var targetRot = Quaternion.LookRotation(target);

            return Quaternion.Slerp(rotation, targetRot, speed);
        }
    }
}