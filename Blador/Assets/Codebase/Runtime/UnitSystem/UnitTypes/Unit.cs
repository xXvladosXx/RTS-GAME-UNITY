using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.TargetSystem;

namespace Codebase.Runtime.UnitSystem
{
    public abstract class Unit<TUnitView> : IGameLoop 
        where TUnitView : UnitView
    {
        public readonly TUnitView UnitView;

        public Team Team { get; set; }

        public Unit(TUnitView unitView)
        {
            UnitView = unitView;
        }

        public virtual bool GameUpdate() => true;

        public void Recycle()
        {
        }
    }
}