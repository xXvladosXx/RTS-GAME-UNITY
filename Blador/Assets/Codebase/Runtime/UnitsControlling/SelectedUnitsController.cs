using System.Collections.Generic;
using Codebase.Runtime.CameraSystem;
using Codebase.Runtime.CameraSystem.Factory;
using Codebase.Runtime.InputSystem;
using Codebase.Runtime.Selection;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UnitSystem;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.UnitsControlling
{
    public class SelectedUnitsController : ITickable
    {
        private readonly ICameraFacade _cameraFacade;
        private readonly SelectableCollector _selectableCollector;
        private readonly ITargetsProvider _targetsProvider;
        private readonly IUnitsMover _unitsMover;
        private readonly IUnitsAttacker _unitsAttacker;
        private readonly IInputProvider _inputProvider;


        public SelectedUnitsController(ICameraFacade cameraFacade,
            SelectableCollector selectableCollector,
            ITargetsProvider targetsProvider,
            IUnitsMover unitsMover,
            IUnitsAttacker unitsAttacker,
            IInputProvider inputProvider)
        {
            _cameraFacade = cameraFacade;
            _selectableCollector = selectableCollector;
            _targetsProvider = targetsProvider;
            _unitsMover = unitsMover;
            _unitsAttacker = unitsAttacker;
            _inputProvider = inputProvider;
        }

        public void Tick()
        {
            if (!_inputProvider.IsRightButtonUp())
                return;

            var raycastHit = _cameraFacade.FireRay(_inputProvider.ReadMousePosition());
            var selectables = _selectableCollector.GetControlledSelectables();

            if (raycastHit.transform.CompareTag("Floor"))
            {
                MoveToPosition(selectables, raycastHit.point);
            }

            if (raycastHit.collider.TryGetComponent(out ITargetAttackable target))
            {
                TryToAttackTarget(selectables, target);
            }
        }

        private void TryToAttackTarget(HashSet<ISelectable> selectables, ITargetAttackable target)
        {
            _unitsAttacker.AttackUnit(selectables, target);
        }

        private void MoveToPosition(HashSet<ISelectable> selectables, Vector3 destination)
        {
            _unitsMover.MoveUnits(selectables, destination);
        }
    }
}