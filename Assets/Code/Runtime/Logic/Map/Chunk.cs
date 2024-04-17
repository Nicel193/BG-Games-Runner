using System;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

namespace Code.Runtime.Logic.Map
{
    public class Chunk : MonoBehaviour
    {
        [field: SerializeField] public Transform Begin { get; private set; }
        [field: SerializeField] public Transform End { get; private set; }
        [field: SerializeField] public ObstacleType AllowedObstacles { get; private set; }

        [HideInInspector] public List<float> obstaclePositions = new List<float>();
        
        public Vector3 GetObstaclePosition(int obstaclePositionIndex)
        {
            if (obstaclePositions.Count <= obstaclePositionIndex)
                throw new IndexOutOfRangeException();

            float positionZ = End.position.z - Begin.position.z;

            return new Vector3(0f, 0f, (positionZ * obstaclePositions[obstaclePositionIndex]) + Begin.position.z);
        }
    }
}