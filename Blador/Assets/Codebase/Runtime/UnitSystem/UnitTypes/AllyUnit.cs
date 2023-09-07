using Codebase.Logic.Entity.StateMachine;
using Codebase.Logic.Entity.StateMachine.Unit;
using Codebase.Runtime.DamageSystem;
using Codebase.Runtime.Movement;
using Codebase.Runtime.TargetSystem;
using UnityEngine;

namespace Codebase.Runtime.UnitSystem
{
    public class AllyUnit : Unit
    {
        public readonly IAttackComponent UnitAttackComponent;

        public EntityStateMachine<Unit> StateMachine { get; private set; }

        public AllyUnit(UnitView unitView, 
            ITargetsProvider targetsProvider,
            IMovement unitMovement,
            IAttackComponent unitAttackComponent) 
            : base(unitView, targetsProvider, unitMovement, unitAttackComponent, Team.Allies)
        {
            UnitAttackComponent = unitAttackComponent;
        }

        protected override void CreateStates()
        {
            StateMachine = new EntityStateMachine<Unit>();

            bool HasTarget() => Target != null;
            bool HasNoTarget() => Target == null;
            bool IsTargetInRange() => UnitAttackComponent.IsInRange(Transform, Target);
            bool IsNotTargetInRange() => !UnitAttackComponent.IsInRange(Transform, Target);

            var idleState = new UnitIdleState(this);
            var moveState = new UnitMoveToState(this);
            var moveTargetState = new UnitMoveToTargetState(this);
            var attackState = new UnitAttackState(this);
            
            StateMachine.AddStates(idleState, moveState, attackState, moveTargetState);

            StateMachine.AddTransition<UnitIdleState, UnitMoveToState>(() => UnitMovement.RemainingDistance > 0.5f);
            StateMachine.AddTransition<UnitMoveToState, UnitIdleState>(() => UnitMovement.RemainingDistance <= 0.5f);
            
            StateMachine.AddTransition<UnitIdleState, UnitMoveToTargetState>(HasTarget);
            
            StateMachine.AddTransition<UnitMoveToState, UnitMoveToTargetState>(HasTarget);
            StateMachine.AddTransition<UnitMoveToTargetState, UnitMoveToState>(HasNoTarget);
            
            StateMachine.AddTransition<UnitMoveToTargetState, UnitAttackState>(IsTargetInRange);
            StateMachine.AddTransition<UnitAttackState, UnitMoveToTargetState>(() => HasTarget() && IsNotTargetInRange());
            StateMachine.AddTransition<UnitAttackState, UnitMoveToState>(HasNoTarget);
            
            StateMachine.SetState<UnitIdleState>();
        }

        public override bool GameUpdate()
        {
            StateMachine.Update();
            UnitView.AnimationStateReader.UpdateState();
            return true;
        }

        public override ITargetAttackable FindNextTarget()
        {
            return new UnitView();
        }
    }
}