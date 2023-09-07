using Codebase.Runtime.AnimatorSystem;
using Codebase.Runtime.UnitSystem;
using UnityEngine;

namespace Codebase.Logic.Entity.StateMachine.Unit
{
    public class UnitAttackState : State<Runtime.UnitSystem.Unit>
    {
        public UnitAttackState(Runtime.UnitSystem.Unit stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnEnter()
        {
            Initializer.UnitMovement.Stop();
            Initializer.UnitView.AnimationStateReader.SetFloat(AnimatorStateHasher.SpeedHash, 0);
        }

        public override void OnUpdate()
        {
            Initializer.AttackComponent.RechargeAttack();
            Initializer.AttackComponent.Attack(Initializer.Target);
        }

        public override void OnExit()
        {
            Initializer.UnitView.AnimationStateReader.PlayAnimation(AnimatorStateHasher.AttackHash, false);
        }
    }
}