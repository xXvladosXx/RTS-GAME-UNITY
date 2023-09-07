using Codebase.Runtime.TargetSystem;

namespace Codebase.Runtime.DamageSystem.Weapon
{
    public interface IWeapon
    {
        IAttackType AttackType { get; }
        void Attack(ITeamMember attacker, ITargetAttackable target);
    }
}