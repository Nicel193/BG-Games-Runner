using Code.Runtime.Configs;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class FollowPlayerCamera : MonoBehaviour, IFollowPlayerCamera
    {
        [SerializeField] private PlayerConfig _cameraConfig;
        [SerializeField] private Transform _playerTransform;

        private void LateUpdate()
        {
            Vector3 targetPosition = _playerTransform.position;
            Vector3 smoothedPosition = Vector3.Lerp(
                new Vector3(transform.position.x, _cameraConfig.Height, transform.position.z),
                new Vector3(targetPosition.x, _cameraConfig.Height, targetPosition.z + _cameraConfig.ZOffset),
                _cameraConfig.SmoothSpeed * Time.deltaTime);
            
            transform.position = smoothedPosition;
        }
    }
}