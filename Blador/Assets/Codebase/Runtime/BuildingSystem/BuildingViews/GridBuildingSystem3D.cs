﻿using System;
using System.Collections.Generic;
using Codebase.Runtime.CameraSystem.Factory;
using Codebase.Runtime.InputSystem;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.BuildingSystem.BuildingViews
{
    public class GridBuildingSystem3D : MonoBehaviour 
    {
        public static GridBuildingSystem3D Instance { get; private set; }

        public event EventHandler OnSelectedChanged;
        public event EventHandler OnObjectPlaced;

        private GridXZ<GridObject> grid;
        [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList = null;
        private PlacedObjectTypeSO placedObjectTypeSO;
        private PlacedObjectTypeSO.Dir dir;
        private ICameraFacade _cameraFacade;
        private IInputProvider _inputProvider;

        [Inject]
        public void Construct(ICameraFacade cameraFacade,
            IInputProvider inputProvider)
        {
            _cameraFacade = cameraFacade;
            _inputProvider = inputProvider;
        }

        private void Awake() {
            Instance = this;

            int gridWidth = 10;
            int gridHeight = 10;
            float cellSize = 10f;
            grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (GridXZ<GridObject> g, int x, int y) => new GridObject(g, x, y));

            placedObjectTypeSO = null;// placedObjectTypeSOList[0];
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0) && placedObjectTypeSO != null) {
                Vector3 mousePosition = _cameraFacade.FireRay(_inputProvider.ReadMousePosition()).point;
                grid.GetXZ(mousePosition, out int x, out int z);

                Vector2Int placedObjectOrigin = new Vector2Int(x, z);
                placedObjectOrigin = grid.ValidateGridPosition(placedObjectOrigin);

                // Test Can Build
                List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(placedObjectOrigin, dir);
                bool canBuild = true;
                foreach (Vector2Int gridPosition in gridPositionList) {
                    if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) {
                        canBuild = false;
                        break;
                    }
                }

                if (canBuild) {
                    Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                    Vector3 placedObjectWorldPosition = grid.GetWorldPosition(placedObjectOrigin.x, placedObjectOrigin.y) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

                    PlacedObjectDone placedObject = PlacedObjectDone.Create(placedObjectWorldPosition, placedObjectOrigin, dir, placedObjectTypeSO);

                    foreach (Vector2Int gridPosition in gridPositionList) {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                    }

                    OnObjectPlaced?.Invoke(this, EventArgs.Empty);

                    //DeselectObjectType();
                } else {
                    // Cannot build here
                    //UtilsClass.CreateWorldTextPopup("Cannot Build Here!", mousePosition);
                }
            }

            if (Input.GetKeyDown(KeyCode.R)) {
                dir = PlacedObjectTypeSO.GetNextDir(dir);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1)) { placedObjectTypeSO = placedObjectTypeSOList[0]; RefreshSelectedObjectType(); }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { placedObjectTypeSO = placedObjectTypeSOList[1]; RefreshSelectedObjectType(); }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { placedObjectTypeSO = placedObjectTypeSOList[2]; RefreshSelectedObjectType(); }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { placedObjectTypeSO = placedObjectTypeSOList[3]; RefreshSelectedObjectType(); }
            if (Input.GetKeyDown(KeyCode.Alpha5)) { placedObjectTypeSO = placedObjectTypeSOList[4]; RefreshSelectedObjectType(); }
            if (Input.GetKeyDown(KeyCode.Alpha6)) { placedObjectTypeSO = placedObjectTypeSOList[5]; RefreshSelectedObjectType(); }

            if (Input.GetKeyDown(KeyCode.Alpha0)) { DeselectObjectType(); }


            if (Input.GetMouseButtonDown(1)) {
                Vector3 mousePosition = _cameraFacade.FireRay(_inputProvider.ReadMousePosition()).point;
                if (grid.GetGridObject(mousePosition) != null) {
                    // Valid Grid Position
                    PlacedObjectDone placedObject = grid.GetGridObject(mousePosition).GetPlacedObject();
                    if (placedObject != null) {
                        // Demolish
                        placedObject.DestroySelf();

                        List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
                        foreach (Vector2Int gridPosition in gridPositionList) {
                            grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
                        }
                    }
                }
            }
        }

        private void DeselectObjectType() {
            placedObjectTypeSO = null; RefreshSelectedObjectType();
        }

        private void RefreshSelectedObjectType() {
            OnSelectedChanged?.Invoke(this, EventArgs.Empty);
        }


        public Vector2Int GetGridPosition(Vector3 worldPosition) {
            grid.GetXZ(worldPosition, out int x, out int z);
            return new Vector2Int(x, z);
        }

        public Vector3 GetMouseWorldSnappedPosition() {
            Vector3 mousePosition = _cameraFacade.FireRay(_inputProvider.ReadMousePosition()).point;
            grid.GetXZ(mousePosition, out int x, out int z);

            if (placedObjectTypeSO != null) {
                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
                return placedObjectWorldPosition;
            } else {
                return mousePosition;
            }
        }

        public Quaternion GetPlacedObjectRotation() {
            if (placedObjectTypeSO != null) {
                return Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0);
            } else {
                return Quaternion.identity;
            }
        }

        public PlacedObjectTypeSO GetPlacedObjectTypeSO() {
            return placedObjectTypeSO;
        }

    }
}
