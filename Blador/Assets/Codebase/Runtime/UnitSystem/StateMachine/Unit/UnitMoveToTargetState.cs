using Codebase.Runtime.AnimatorSystem;
using Codebase.Runtime.UnitSystem;
using UnityEngine;

namespace Codebase.Logic.Entity.StateMachine.Unit
{
    public class UnitMoveToTargetState : State<Runtime.UnitSystem.Unit>
    {
        public UnitMoveToTargetState(Runtime.UnitSystem.Unit stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnUpdate()
        {
            Initializer.UnitView.AnimationStateReader.SetFloat(AnimatorStateHasher.SpeedHash, 1, .2f, Time.deltaTime);
            Initializer.UnitMovement.MoveTo(Initializer.Target.Position);
        }
    }
}