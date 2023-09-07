using Codebase.Runtime.Selection;
using Codebase.Runtime.TargetSystem;

namespace Codebase.Runtime.GameplayCore
{
    public interface ILevelBinder
    {
        ITargetsProvider TargetsProvider { get; set; }
        IUnitsKeeper UnitsKeeper { get; set; }
        SelectableCollector SelectableCollector { get; set; }
    }
}