using UnityEngine;

namespace Codebase.Runtime.TargetSystem
{
    public interface ITeamMember
    {
        Team Team { get; }
        Transform Transform { get; }
    }
}