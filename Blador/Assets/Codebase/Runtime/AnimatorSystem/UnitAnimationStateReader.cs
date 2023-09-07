using UnityEngine;
using UnityEngine.AI;

namespace Codebase.Runtime.AnimatorSystem
{
    public class UnitAnimationStateReader : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _moveDampTime = .2f;

        public AnimatorState State { get; private set; }
        public Animator Animator => _animator;

        public void PlayAnimation(int hash, bool value)
        {
            _animator.SetBool(hash, value);
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
        }

        public void UpdateState()
        {
        }

        public void ExitedState(int stateHash)
        {
        }
        
        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == AnimatorStateHasher.SpeedHash)
            {
                state = AnimatorState.Idle;
            }
            else if (stateHash == AnimatorStateHasher.AttackHash)
            {
                state = AnimatorState.Attack;
            }
            else
            {
                state = AnimatorState.Unknown;
            }

            return state;
        }

        public void SetFloat(int speedHash, int value, float dampTime, float deltaTime)
        {
            _animator.SetFloat(speedHash, value, dampTime, deltaTime);
        }

        public void SetFloat(int speedHash, int value)
        {
            _animator.SetFloat(speedHash, value);
        }
    }
}