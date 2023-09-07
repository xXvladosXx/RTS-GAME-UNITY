using Codebase.Runtime.TargetSystem;
using UnityEngine;

namespace Codebase.Runtime.DamageSystem
{
    public interface IAttackComponent
    {
        void Attack(ITargetAttackable target);
        void RechargeAttack();
        bool IsInRange(Transform from, ITargetAttackable target);
    }
}