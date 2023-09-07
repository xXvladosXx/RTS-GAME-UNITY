using System;
using Codebase.Runtime.CameraSystem.Factory;
using Codebase.Runtime.UnitSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Zenject;

namespace Codebase.Runtime.Selection.NewSelection
{
    public class UnitSelector : IUnitSelector
    {
        private ICameraFacade _cameraFacade;
        private SelectableCollector _selectableCollector;
        private Vector3 _startPosition;
        private Vector3 _currentPosition;
        private Bounds _selectionBounds = new Bounds();

        public UnitSelector(ICameraFacade cameraFacade,
            SelectableCollector selectableCollector)
        {
            _cameraFacade = cameraFacade;
            _selectableCollector = selectableCollector;
        }

        public void OnStartSelect(Vector3 position)
        {
            _startPosition = position;
        }

        public void OnSelecting(Vector3 position)
        {
            _currentPosition = position;
            SetBounds();
            CheckIntersections();
        }

        private void SetBounds()
        {
            var v1 = _cameraFacade.CameraMain.Camera.ScreenToViewportPoint(_startPosition);
            var v2 = _cameraFacade.CameraMain.Camera.ScreenToViewportPoint(_currentPosition);

            var min = Vector3.Min(v1, v2);
            var max = Vector3.Max(v1, v2);

            min.z = _cameraFacade.CameraMain.Camera.nearClipPlane;
            max.z = _cameraFacade.CameraMain.Camera.farClipPlane;

            _selectionBounds.SetMinMax(min, max);
        }

        private void CheckIntersections()
        {
            foreach (var character in _selectableCollector.AvailableEntities)
            {
                var characterInBounds = _selectionBounds.Contains(
                    _cameraFacade.CameraMain.Camera.WorldToViewportPoint(character.Position)
                );

                if (characterInBounds)
                {
                    if (character.IsSelected)
                        continue;
                    
                    character.Select();
                    _selectableCollector.AddSelected(character);
                }
                else
                {
                    if(EventSystem.current.IsPointerOverGameObject())
                        continue;
                    
                    if (!character.IsSelected)
                        continue;

                    character.Deselect();
                    _selectableCollector.RemoveSelected(character);
                }
            }
        }

        public void OnLeftClick(Vector3 position)
        {
            var hit = _cameraFacade.FireRay(position, LayerMask.GetMask("Units"));

            if (hit.transform != null)
            {
                Select(hit.transform.gameObject.GetComponent<ISelectable>());
            }
            else
            {
                Deselect();
            }
        }

        private void Select(ISelectable player)
        {
            if (player == null) return;
            ReplaceSelected(player);
        }

        private void Deselect()
        {
            foreach (var selectable in _selectableCollector.AvailableEntities)
            {
                selectable.Deselect();
                _selectableCollector.RemoveSelected(selectable);
            }
        }

        private void ReplaceSelected(ISelectable selected)
        {
            selected.Select();
        }
    }
}