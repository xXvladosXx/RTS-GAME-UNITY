
using Codebase.Runtime.UnitSystem;

namespace Codebase.Runtime.TargetSystem
{
    public interface ITargetsProvider 
    {
        UnitView GetUnitTargetFor(ITeamMember member);
        void OnTargetCreated(ITargetAttackable unitView, Team team);
        bool CheckIfCanBeAttacked(ITargetAttackable target, Team team);
    }
}