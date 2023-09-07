using System.Collections.Generic;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.UnitSystem;

namespace Codebase.Runtime.TargetSystem
{
    public class TargetsProvider : ITargetsProvider
    {
        private Dictionary<ITargetAttackable, Team> _targets = new();
        private readonly IUnitsKeeper _unitsKeeper;
        
        public TargetsProvider(IUnitsKeeper unitsKeeper,
            ILevelBinder levelBinder)
        {
            _unitsKeeper = unitsKeeper;
            levelBinder.TargetsProvider = this;
        }

        public UnitView GetUnitTargetFor(ITeamMember member) => 
            _unitsKeeper.FindClosestUnit(member.Team, member.Transform.position);

        public void OnTargetCreated(ITargetAttackable unitView, Team team)
        {
            _targets.Add(unitView, team);    
        }

        public void OnTargetRemoved(ITargetAttackable unitView)
        {
            _targets.Remove(unitView);    
        }

        public bool CheckIfCanBeAttacked(ITargetAttackable target, Team team)
        {
            _targets.TryGetValue(target, out var targetTeam);
            return targetTeam != team;
        }
        
    }
}