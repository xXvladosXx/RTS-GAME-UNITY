using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Runtime.DamageSystem.Weapon
{
    public interface IAttackApplyForm
    {
        (Collider[], int) FindObjects(Transform user, Vector3 target);
    }
}