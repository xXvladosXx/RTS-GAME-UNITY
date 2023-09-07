using System.Collections.Generic;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UnitSystem;

namespace Codebase.Runtime.UnitsControlling
{
    public interface IUnitsAttacker
    {
        void AttackUnit(HashSet<ISelectable> selectables, ITargetAttackable target);
    }
}