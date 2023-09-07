using Codebase.Runtime.TargetSystem;

namespace Codebase.Runtime.UnitSystem
{
    public class AllyView : UnitView
    {
        public override bool PossibleToControl => true;

        public override bool CanBeAttackedBy(ITeamMember attacker) => attacker.Team == Team.Enemies;
    }
}