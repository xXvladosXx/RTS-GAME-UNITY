using System;
using System.Collections.Generic;
using Codebase.Runtime.DamageSystem.Weapon.Range;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.TargetSystem;

namespace Codebase.Runtime.DamageSystem.Weapon
{
    public interface IAttackType
    {
        public void Attack(ITeamMember attacker, ITargetAttackable target);
    }

    public class AOEAttackType : IAttackType
    {
        public void Attack(ITeamMember attacker, ITargetAttackable target)
        {
        }
    }

    public class SingleAttackType : IAttackType
    {
        public void Attack(ITeamMember attacker, ITargetAttackable target)
        {
            target.ApplyDamage(attacker, 10);
        }
    }

    public class ProjectileAttackType : IAttackType
    {
        private readonly IObjectPool _objectPool;
        private readonly Projectile _projectile;

        public ProjectileAttackType(IObjectPool objectPool,
            Projectile projectile)
        {
            _objectPool = objectPool;
            _projectile = projectile;
        }

        public void Attack(ITeamMember attacker, ITargetAttackable target)
        {
            if (_projectile == null)
                return;

            if (_objectPool == null)
                throw new ArgumentNullException(nameof(_objectPool));

            var arrow = _objectPool.GetOrCreate(_projectile, attacker.Transform.position, attacker.Transform.rotation);
            arrow.Setup(target, attacker, 10);
        }
    }
}