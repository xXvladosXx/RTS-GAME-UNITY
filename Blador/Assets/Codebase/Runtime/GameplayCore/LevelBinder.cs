using Codebase.Runtime.Selection;
using Codebase.Runtime.TargetSystem;

namespace Codebase.Runtime.GameplayCore
{
    public class LevelBinder : ILevelBinder
    {
        public ITargetsProvider TargetsProvider { get; set; }
        public IUnitsKeeper UnitsKeeper { get; set; }
        public SelectableCollector SelectableCollector { get; set; }
    }
}