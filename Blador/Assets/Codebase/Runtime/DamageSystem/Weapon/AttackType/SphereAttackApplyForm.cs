using System;
using UnityEngine;

namespace Codebase.Runtime.DamageSystem.Weapon
{
    [Serializable]
    public class SphereAttackApplyForm : IAttackApplyForm
    {
        public float Radius;
        public float Offset;
        public Direction Direction;
        
        private Collider[] _maxColliders = new Collider[MAX_COLLIDERS];
        private const int MAX_COLLIDERS = 100;

        public (Collider[], int) FindObjects(Transform user, Vector3 target)
        {
            var direction = Direction switch
            {
                Direction.Forward => user.forward,
                Direction.Backward => -user.forward,
                Direction.Left => -user.right,
                Direction.Right => user.right,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            var results = Physics.OverlapSphereNonAlloc(target + (direction * Offset), Radius, _maxColliders);
            return (_maxColliders, results);
        }
    }
}