using System;
using System.Collections.Generic;
using Codebase.Runtime.UnitSystem;
using UnityEngine;

namespace Codebase.Runtime.Selection
{
    public interface IUnitFormation
    {
        void FormUnits(List<Unit> units, Vector3 destinationTo, float formationOffset, UnitFormation.FormationModes formationMode = UnitFormation.FormationModes.Rectangle);
    }

    public class UnitFormation : IUnitFormation
    {
        public enum FormationModes : int
        {
            Rectangle,
            HexGrid,
            Circle,
        }

#if UNITY_EDITOR
        private struct Location
        {
            public readonly Vector3 Position;
            public readonly Quaternion Rotation;

            public Location(Vector3 pos, Quaternion rot)
            {
                Position = pos;
                Rotation = rot;
            }
        }
#endif

#if UNITY_EDITOR
        public Mesh DebugMesh;
        private Location[] _debugCommandLocations = Array.Empty<Location>();
#endif

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            for (int i = 0; i < _debugCommandLocations.Length && DebugMesh != null; i++)
            {
                Gizmos.color = Color.green.ToWithA(0.3f);
                Gizmos.DrawMesh(DebugMesh, _debugCommandLocations[i].Position, _debugCommandLocations[i].Rotation,
                    new Vector3(1f, .1f, 1f));
            }
        }
#endif

        public void FormUnits(List<Unit> units, Vector3 destinationTo, 
            float formationOffset, FormationModes formationMode = FormationModes.Rectangle)
        {
#if UNITY_EDITOR
            _debugCommandLocations = Array.Empty<Location>();
#endif

            Vector3 destination = destinationTo;
            Vector3 origin;
            Vector3[] positions = new Vector3[units.Count];
            for (int i = 0; i < units.Count; i++)
            {
                positions[i] = units[i].Transform.position;
            }

            origin = units.Count == 1 ? positions[0] : positions.FindCentroid();
            Quaternion rotation = Quaternion.LookRotation((destination - origin).normalized);
            Vector3[] offsets = GetFormationOffsets(units, formationOffset, formationMode);
#if UNITY_EDITOR
            _debugCommandLocations = new Location[offsets.Length];
            for (int i = 0; i < offsets.Length; i++)
            {
                _debugCommandLocations[i] = new Location(destination + rotation * offsets[i], rotation);
            }
#endif

            int[,] assignments = new int[units.Count, offsets.Length];
            for (int i = 0; i < units.Count; i++)
            {
                for (int j = 0; j < offsets.Length; j++)
                {
                    Vector3 newPos = origin + rotation * offsets[i];
                    assignments[i, j] = 1000 - Mathf.RoundToInt(Vector3.Distance(units[i].Transform.position, newPos));
                }
            }

            int[] results = HungarianAlgorithm.HungarianAlgorithm.FindAssignments(assignments);

            for (int i = 0; i < results.Length; i++)
            {
                units[i].UnitMovement.MoveTo(destination + rotation * offsets[results[i]]);
            }
        }

        public Vector3[] GetFormationOffsets(List<Unit> units, float formationOffset, FormationModes formationMode)
        {
            int count = units.Count;
            Vector3[] offsets = new Vector3[count];

            int caseCounter = 0;
            switch (formationMode)
            {
                default:
                case FormationModes.Circle:
                {
                    offsets[0] = Vector3.zero;
                    int index = 1;
                    int remaining = count - 1;
                    float currentOffset = formationOffset;
                    for (int j = 0; remaining > 0; j++)
                    {
                        currentOffset = formationOffset * (j + 1);
                        float circumfence = 2f * Mathf.PI * currentOffset;
                        int spaces = Mathf.FloorToInt(circumfence / formationOffset);
                        float angle = 360f / (spaces > remaining ? remaining : spaces);

                        for (int i = 0; i < spaces && index < count; i++)
                        {
                            float rotationOffset = angle * (j % 2f) * 0.5f;
                            offsets[index] = new Vector3(
                                (angle * i + rotationOffset).Sin() * currentOffset,
                                0f,
                                (angle * i + rotationOffset).Cos() * currentOffset
                            );
                            remaining--;
                            index++;
                        }
                    }
                }
                    break;
                case FormationModes.Rectangle:
                {
                    float sqrt = Mathf.Sqrt(count);
                    int i = 0;
                    float currentOffset = formationOffset;

                    int h = Mathf.FloorToInt(sqrt);
                    float tmp = count / (float) h;
                    int w = Mathf.CeilToInt(tmp);

                    for (int y = h - 1; y >= 0 && i < count; y--)
                    {
                        int remaining = (count - i);
                        float x = 0;
                        if (remaining < w && formationMode == FormationModes.Rectangle)
                        {
                            currentOffset = formationOffset * (w - 1f) / (remaining - 1);
                        }

                        for (; x < w && i < count; x++, i++)
                        {
                            offsets[i] = new Vector3(
                                x * currentOffset - (w - 1f) * 0.5f * formationOffset,
                                0f,
                                y * currentOffset - (h - 1f) * 0.5f * formationOffset
                            );
                        }
                    }
                }
                    if (formationMode == FormationModes.HexGrid)
                    {
                        caseCounter++;
                        goto case FormationModes.HexGrid;
                    }

                    break;
                case FormationModes.HexGrid:
                {
                    if (caseCounter == 0)
                    {
                        goto case FormationModes.Rectangle;
                    }

                    float halfFormationOffset = formationOffset / 2f;
                    float triangleHeightOffset =
                        Mathf.Sqrt(Mathf.Pow(formationOffset, 2f) - Mathf.Pow(halfFormationOffset, 2f));

                    float lastY = offsets[0].z;
                    bool toggle = true;
                    for (int i = 0; i < count; i++)
                    {
                        Vector3 offset = offsets[i];
                        if (offset.z != lastY)
                        {
                            toggle = !toggle;
                        }

                        lastY = offset.z;
                        offset.x += halfFormationOffset * toggle.ToSignFloat() * 0.5f;
                        offset.z = (offset.z / formationOffset) * triangleHeightOffset;
                        offsets[i] = offset;
                    }

                    break;
                }
            }

            return offsets;
        }

    }
}