﻿using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Runtime.BuildingSystem.BuildingViews
{
    public class PlacedObjectDone : MonoBehaviour {

        public static PlacedObjectDone Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectTypeSO) {
            Transform placedObjectTransform = Instantiate(placedObjectTypeSO.prefab, worldPosition, Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0));

            PlacedObjectDone placedObject = placedObjectTransform.GetComponent<PlacedObjectDone>();
            placedObject.Setup(placedObjectTypeSO, origin, dir);

            return placedObject;
        }

        private PlacedObjectTypeSO placedObjectTypeSO;
        private Vector2Int origin;
        private PlacedObjectTypeSO.Dir dir;

        private void Setup(PlacedObjectTypeSO placedObjectTypeSO, Vector2Int origin, PlacedObjectTypeSO.Dir dir) {
            this.placedObjectTypeSO = placedObjectTypeSO;
            this.origin = origin;
            this.dir = dir;
        }

        public List<Vector2Int> GetGridPositionList() {
            return placedObjectTypeSO.GetGridPositionList(origin, dir);
        }

        public void DestroySelf() {
            Destroy(gameObject);
        }

        public override string ToString() {
            return placedObjectTypeSO.nameString;
        }

    }
}