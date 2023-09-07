using System;
using Codebase.Logic.Entity.StateMachine;
using Codebase.Logic.Entity.StateMachine.Unit;
using Codebase.Runtime.DamageSystem;
using Codebase.Runtime.Movement;
using Codebase.Runtime.TargetSystem;

namespace Codebase.Runtime.UnitSystem
{
    public class EnemyUnit : Unit
    {
        public readonly IAttackComponent UnitAttackComponent;

        public EntityStateMachine<Unit> StateMachine { get; private set; }

        public EnemyUnit(UnitView unitView,
            ITargetsProvider targetsProvider,
            IMovement unitMovement,
            IAttackComponent unitAttackComponent) 
            : base(unitView, targetsProvider, unitMovement, unitAttackComponent, Team.Enemies)
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
            var moveState = new UnitMoveToTargetState(this);
            var attackState = new UnitAttackState(this);
            
            StateMachine.AddStates(idleState, moveState, attackState);

            StateMachine.AddTransition<UnitIdleState, UnitMoveToTargetState>(HasTarget);
            StateMachine.AddTransition<UnitMoveToTargetState, UnitIdleState>(HasNoTarget);
            
            StateMachine.AddTransition<UnitMoveToTargetState, UnitAttackState>(IsTargetInRange);
            StateMachine.AddTransition<UnitAttackState, UnitMoveToTargetState>(IsNotTargetInRange);
            
            StateMachine.SetState<UnitIdleState>();
        }

        public override bool GameUpdate()
        {
            StateMachine.Update();
            return base.GameUpdate();
        }

        public override ITargetAttackable FindNextTarget()
        {
            return TargetsProvider.GetUnitTargetFor(UnitView);
        }

    }
}