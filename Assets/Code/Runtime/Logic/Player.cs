using UnityEngine;

namespace Code.Runtime.Logic
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed = 5f;

        void Update()
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}