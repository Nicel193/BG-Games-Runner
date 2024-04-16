using Code.Runtime.Configs;
using Code.Runtime.Services.InputService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;

        private PlayerStateMachine _playerStateMachine;
        private PlayerSideMovement _playerSideMovement;

        private Rigidbody _rigidbody;
        private IInputService _inputService;

        private void Awake()
        {
            _rigidbody = this.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _playerSideMovement = new PlayerSideMovement(_rigidbody, _inputService,
                playerConfig.SideMoveOffset, playerConfig.ChangeSideSpeed);
        }

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Update()
        {
            _playerSideMovement.UpdatePosition();
            
            transform.Translate(Vector3.forward * (playerConfig.StartMoveSpeed * Time.deltaTime));
        }
    }
}