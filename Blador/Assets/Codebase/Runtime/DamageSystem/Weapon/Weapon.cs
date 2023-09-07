using Codebase.Runtime.TargetSystem;

namespace Codebase.Runtime.DamageSystem.Weapon
{
    public class Weapon<T> : Weapon where T : WeaponData
    {
        public T AttackData { get; }
        public Weapon(IAttackType attackType,
            T attackData) : base(attackType)
        {
            AttackData = attackData;
        }

        public override void Attack(ITeamMember attacker, ITargetAttackable target)
        {
            AttackType.Attack(attacker, target);
        }
    }

    public abstract class Weapon : IWeapon
    {
        public IAttackType AttackType { get; }

        public Weapon(IAttackType attackType)
        {
            AttackType = attackType;
        }

        public abstract void Attack(ITeamMember attacker, ITargetAttackable target);
    }
}