using UnityEngine;
using UnityEngine.AI;

namespace Codebase.Runtime.Movement
{
    public class UnitMovement : IMovement
    {
        private readonly NavMeshAgent _navMeshAgent;

        public bool IsStopped => _navMeshAgent.isStopped;

        public UnitMovement(NavMeshAgent navMeshAgent,
            float speed)
        {
            _navMeshAgent = navMeshAgent;
            _navMeshAgent.speed = speed;
        }
        
        public float RemainingDistance => _navMeshAgent.remainingDistance;

        public void MoveTo(Vector3 destination, float speed)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.speed = speed;
            _navMeshAgent.isStopped = false;
        }

        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.isStopped = false;
        }
        
        public void Stop()
        {
            _navMeshAgent.isStopped = true;
        }
    }
}