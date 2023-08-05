namespace Codebase.Runtime.TargetSystem
{
    public class TargetsProvider : ITargetsProvider
    {
        private readonly IUnitsKeeper _unitsKeeper;
        
        public TargetsProvider(IUnitsKeeper unitsKeeper)
        {
            _unitsKeeper = unitsKeeper;
        }
        
        public ITarget GetUnitTargetFor(ITeamMember member)
        {
            return null;
        }
    }
}