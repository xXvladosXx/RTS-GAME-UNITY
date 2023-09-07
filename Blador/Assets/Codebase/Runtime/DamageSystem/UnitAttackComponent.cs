using Codebase.Runtime.AnimatorSystem;
using Codebase.Runtime.DamageSystem.Weapon;
using Codebase.Runtime.TargetSystem;
using UnityEngine;

namespace Codebase.Runtime.DamageSystem
{
    public class UnitAttackComponent : IAttackComponent
    {
        private readonly IWeapon _weapon;
        private readonly IAnimationStateReader _animationStateReader;
        private readonly float _attackSpeed;
        private readonly float _attackRange;
        private float _attackRecharge;
        private readonly ITeamMember _teamMember;

        public const float ATTACK_RECHARGE_MAX = 1f;

        public UnitAttackComponent( 
            float attackSpeed,
            float attackRange,
            ITeamMember teamMember,
            IWeapon weapon,
            IAnimationStateReader animationStateReader)
        {
            _attackSpeed = attackSpeed;
            _attackRange = attackRange;
            _teamMember = teamMember;
            _weapon = weapon;
            _animationStateReader = animationStateReader;
        }

        public void RechargeAttack()
        {
            if (_attackRecharge < ATTACK_RECHARGE_MAX)
            {
                _attackRecharge += _attackSpeed * Time.deltaTime;
            }
        }

        public bool IsInRange(Transform from, ITargetAttackable target) => 
            Vector3.Distance(from.position, target.Position) < _attackRange;

        public void Attack(ITargetAttackable target)
        {
            if (_attackRecharge < ATTACK_RECHARGE_MAX)
            {
                _animationStateReader.PlayAnimation(AnimatorStateHasher.AttackHash, false);
                return;
            }
            
            _animationStateReader.PlayAnimation(AnimatorStateHasher.AttackHash, true);
            _weapon.Attack(_teamMember, target);
            _attackRecharge = 0f;
        }
    }
}