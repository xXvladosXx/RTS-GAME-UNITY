using System;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.Utils;

namespace Codebase.Runtime.DamageSystem.Weapon.Range
{
    public class Arrow : Projectile
    {
        private float _damage;

        public void ArrowSetup(ITargetAttackable target, ITeamMember owner, float speed, float damage)
        {
            Setup(target, owner, speed);

            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage), "Must be greater than or equal to 0.");

            _damage = damage;
        }
        
        protected override void OnFinishAction()
        {
            if (Target.IsNullOrMissing())
                return;
            
            Target.ApplyDamage(BulletOwner, _damage);
        }
    }
}