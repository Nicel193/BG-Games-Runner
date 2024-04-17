using System.Collections.Generic;
using Code.Runtime.Logic.Map;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(Chunk))]
    public class ChunkEditor : UnityEditor.Editor
    {
        private const float SphereRadius = 0.5f;
        
        private Chunk _chunk;

        public void OnEnable()
        {
            _chunk = target as Chunk;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Add obstacles"))
            {
                _chunk.obstaclePositions.Add(0);
            }

            if (_chunk.obstaclePositions != null)
            {
                int toremove = -1;
                for (int i = 0; i < _chunk.obstaclePositions.Count; ++i)
                {
                    GUILayout.BeginHorizontal();
                    _chunk.obstaclePositions[i] = EditorGUILayout.Slider(_chunk.obstaclePositions[i], 0.0f, 1.0f);
                    if (GUILayout.Button("-", GUILayout.MaxWidth(32)))
                        toremove = i;
                    GUILayout.EndHorizontal();
                }

                if (toremove != -1)
                    _chunk.obstaclePositions.RemoveAt(toremove);
            }

            SetDirtyChunk();
        }

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void DrawCustomGizmo(Chunk chunk, GizmoType gizmoType)
        {
            List<float> chunkObstaclePositions = chunk.obstaclePositions;

            for (int i = 0; i < chunkObstaclePositions.Count; i++)
            {
                Vector3 obstaclePosition = chunk.GetObstaclePosition(i);

                Gizmos.color = Color.green;
                Gizmos.DrawSphere(obstaclePosition, SphereRadius);
            }
        }
        
        private void SetDirtyChunk()
        {
            if (GUI.changed)
                EditorUtility.SetDirty(_chunk);
        }
    }
}