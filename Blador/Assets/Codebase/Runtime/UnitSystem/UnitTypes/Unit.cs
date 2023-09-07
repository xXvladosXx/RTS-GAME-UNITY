using Codebase.Logic.Entity.StateMachine;
using Codebase.Logic.Entity.StateMachine.Unit;
using Codebase.Runtime.DamageSystem;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.Movement;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.Utils;
using UnityEngine;

namespace Codebase.Runtime.UnitSystem
{
    public abstract class Unit : IGameLoop
    {
        public readonly IMovement UnitMovement;
        public readonly IAttackComponent AttackComponent;
        public readonly UnitView UnitView;
        private float _lastTargetFindTime;

        public ITargetsProvider TargetsProvider { get; }
        public Vector3 MoveToPosition { get; set; }
        public ITargetAttackable Target => UnitView.Target;
        public Transform Transform => UnitView.transform;

        public const float DEFAULT_FINDING_TARGET_RATE = 2f;

        public Unit(UnitView unitView,
            ITargetsProvider targetsProvider,
            IMovement unitMovement,
            IAttackComponent attackComponent,
            Team team)
        {
            TargetsProvider = targetsProvider;
            UnitView = unitView;
            UnitMovement = unitMovement;
            AttackComponent = attackComponent;
            UnitView.Initialize(team);
        }

        public void Initialize()
        {
            CreateStates();
        }

        protected abstract void CreateStates();


        public virtual bool GameUpdate()
        {
            FindTarget();
            return true;
        }

        private void FindTarget()
        {
            if (!Target.IsNullOrMissing()
                && Time.timeSinceLevelLoad - _lastTargetFindTime < (1f / DEFAULT_FINDING_TARGET_RATE))
                return;
            
            _lastTargetFindTime = Time.timeSinceLevelLoad;
            var newTarget = FindNextTarget();
            SetNewTarget(newTarget);
        }

        private void SetNewTarget(ITargetAttackable newTarget)
        {
            if(Target == newTarget)
                return;
            
            UnitView.Target = newTarget;
        }

        public abstract ITargetAttackable FindNextTarget();

        public void Recycle()
        {
        }
    }
}