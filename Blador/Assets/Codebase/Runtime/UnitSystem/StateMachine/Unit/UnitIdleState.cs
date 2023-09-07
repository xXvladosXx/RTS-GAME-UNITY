using Codebase.Runtime.AnimatorSystem;
using Codebase.Runtime.UnitSystem;
using UnityEngine;

namespace Codebase.Logic.Entity.StateMachine.Unit
{
    public class UnitIdleState : State<Runtime.UnitSystem.Unit>
    {
        public UnitIdleState(Runtime.UnitSystem.Unit stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnUpdate()
        {
            Initializer.UnitView.AnimationStateReader.SetFloat(AnimatorStateHasher.SpeedHash, 0, .2f, Time.deltaTime);
            Initializer.UnitMovement.Stop();
        }
    }
}