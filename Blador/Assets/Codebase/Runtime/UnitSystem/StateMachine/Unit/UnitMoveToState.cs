using Codebase.Runtime.AnimatorSystem;
using UnityEngine;

namespace Codebase.Logic.Entity.StateMachine.Unit
{
    public class UnitMoveToState : State<Runtime.UnitSystem.Unit>
    {
        public UnitMoveToState(Runtime.UnitSystem.Unit stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnUpdate()
        {
            Initializer.UnitView.AnimationStateReader.SetFloat(AnimatorStateHasher.SpeedHash, 1, .2f, Time.deltaTime);
        }
    }
}