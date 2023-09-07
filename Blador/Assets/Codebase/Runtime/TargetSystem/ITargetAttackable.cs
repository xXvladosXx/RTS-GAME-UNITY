using UnityEngine;

namespace Codebase.Runtime.TargetSystem
{
    public interface ITargetAttackable
    {
        Vector3 Position { get; }
        ITargetAttackable Target { get; set; }
        bool CanBeAttackedBy(ITeamMember attacker);
        void ApplyDamage(ITeamMember attacker, float damage);
    }
}