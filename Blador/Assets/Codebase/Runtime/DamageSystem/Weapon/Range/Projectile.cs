using System;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.Utils;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.DamageSystem.Weapon.Range
{
    public abstract class Projectile : MonoBehaviour, IPooledObject
    {
        public event Action<Component> OnDestroyAsPooledObject;
        public int PrefabInstanceID { get; set; }
        
        protected ITargetAttackable Target;
        protected ITeamMember BulletOwner;
        protected float Speed;

        public const float DEFAULT_MINIMAL_DISTANCE_TO_POINT = 0.1f;
        public const float DEFAULT_ROTATION_SPEED_MULTIPLIER = 2f;

        public virtual void Setup(ITargetAttackable target,
            ITeamMember bulletOwner,
            float speed)
        {
            if (speed < 0)
                throw new ArgumentOutOfRangeException(nameof(speed), "Must be greater than or equal to 0.");

            Target = target;
            BulletOwner = bulletOwner;
            Speed = speed;
        }

        private void Update()
        {
            if (Target.IsNullOrMissing())
            {
                DestroyAsPooledObject();
                return;
            }

            Move();
        }

        public void DestroyAsPooledObject() => 
            OnDestroyAsPooledObject?.Invoke(this);

        protected virtual void Finish()
        {
            OnFinishAction();
            DestroyAsPooledObject();
        }

        protected abstract void OnFinishAction();

        private void Move()
        {
            var point = Target.Position;
            var distance = transform.position.FlatDistanceTo(point);

            if (distance > DEFAULT_MINIMAL_DISTANCE_TO_POINT)
            {
                transform.rotation = transform.rotation.FlatRotateTowardsTo(
                    point - transform.position, Speed * Time.deltaTime * DEFAULT_ROTATION_SPEED_MULTIPLIER);

                transform.position = transform.position.FlatMoveTowardsTo(point, Speed * Time.deltaTime);
            }
            else
            {
                Finish();
            }
        }
    }
}