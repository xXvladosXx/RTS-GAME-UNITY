using System;
using Codebase.Runtime.AnimatorSystem;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.TargetSystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Codebase.Runtime.UnitSystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitView : MonoBehaviour, ISelectable, IPooledObject, ITarget, ITeamMember, ITargetAttackable
    {
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field: SerializeField] public UnitAnimationStateReader AnimationStateReader { get; private set; }
        [SerializeField] private GameObject _selectionIndicator;
        
        public int PrefabInstanceID { get; set; }
        public Team Team { get; private set; }

        [field: SerializeField] public SelectableData Data { get; private set; }
        public Vector3 Position => transform.position;
        public Collider Collider => GetComponent<Collider>();
        public bool IsSelected { get; private set; }
        public virtual bool PossibleToControl => false;
        public Transform Transform => transform;
        public event Action<Component> OnDestroyAsPooledObject;

        public void Initialize(Team team)
        {
            Team = team;
        }

        private void OnValidate()
        {
            if(NavMeshAgent == null)
                NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Select()
        {
            IsSelected = true;
            _selectionIndicator.SetActive(true);
        }

        public void Deselect()
        {
            IsSelected = false;
            _selectionIndicator.SetActive(false);
        }

        public void DestroyAsPooledObject()
        {
            
        }

        public ITargetAttackable Target { get; set; }
        public virtual bool CanBeAttackedBy(ITeamMember attacker) => attacker.Team == Team.Allies;
        public void ApplyDamage(ITeamMember attacker, float damage)
        {
           Debug.Log("Received damage");
        }
    }
}