
namespace Codebase.Runtime.TargetSystem
{
    public interface ITargetsProvider 
    {
        ITarget GetUnitTargetFor(ITeamMember member);
    }
}