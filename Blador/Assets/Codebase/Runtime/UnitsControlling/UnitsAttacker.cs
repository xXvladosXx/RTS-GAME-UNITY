using System.Collections.Generic;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UnitSystem;

namespace Codebase.Runtime.UnitsControlling
{
    public class UnitsAttacker : IUnitsAttacker
    {
        private readonly IUnitsKeeper _unitsKeeper;
        private readonly ITargetsProvider _targetsProvider;

        public UnitsAttacker(IUnitsKeeper unitsKeeper,
            ITargetsProvider targetsProvider)
        {
            _unitsKeeper = unitsKeeper;
            _targetsProvider = targetsProvider;
        }

        public void AttackUnit(HashSet<ISelectable> selectables, ITargetAttackable target)
        {
            foreach (var selectable in selectables)
            {
                if (selectable is not ITeamMember teamMember)
                    continue;
                if (!_targetsProvider.CheckIfCanBeAttacked(target, teamMember.Team))
                    continue;
                
                if(selectable is ITargetAttackable targetAttackable)
                    targetAttackable.Target = target;
            }
        }
    }
}