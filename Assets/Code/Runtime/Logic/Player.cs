using Code.Runtime.Configs;
using Code.Runtime.Services.InputService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;

        private PlayerStateMachine _playerStateMachine;
        private PlayerSideMovement _playerSideMovement;

        private Rigidbody _rigidbody;
        private IInputService _inputService;
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _rigidbody = this.GetComponent<Rigidbody>();
            _boxCollider = this.GetComponent<BoxCollider>();
        }

        private void Start()
        {
            _playerSideMovement = new PlayerSideMovement(_rigidbody, _inputService,
                playerConfig.SideMoveOffset, playerConfig.ChangeSideSpeed);
            _playerStateMachine = new PlayerStateMachine();
            
            RegisterStates();
        }

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void RegisterStates()
        {
            _playerStateMachine.RegisterState(new RunState(_rigidbody, _inputService, _playerStateMachine));
            _playerStateMachine.RegisterState(new JumpState(_rigidbody, _inputService, _playerStateMachine, 5f));
            _playerStateMachine.RegisterState(new SlidingState(_rigidbody, _inputService, _playerStateMachine, _boxCollider, 0.5f));
            
            _playerStateMachine.Enter<RunState>();
        }

        public void Update()
        {
            _playerStateMachine.UpdateState();
            _playerSideMovement.UpdatePosition();

            transform.Translate(Vector3.forward * (playerConfig.StartMoveSpeed * Time.deltaTime));
        }
    }
}