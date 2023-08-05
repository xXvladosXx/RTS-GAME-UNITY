using Codebase.Runtime.Movement;
using Codebase.Runtime.UnitSystem.Data.Enemy;
using UnityEngine;

namespace Codebase.Runtime.UnitSystem
{
    public class EnemyUnit : Unit<UnitView>
    {
        private readonly UnitMovement _unitMovement;

        public EnemyUnit(UnitView unitView,
            UnitMovement unitMovement)
            : base(unitView)
        {
            _unitMovement = unitMovement;
        }

        public override bool GameUpdate()
        {
            _unitMovement.MoveTo(Vector3.down, 2);
            return true;
        }
    }
}