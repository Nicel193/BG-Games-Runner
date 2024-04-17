using System;
using Code.Runtime.Configs;
using Code.Runtime.Logic.Map;
using Code.Runtime.Logic.PlayerSystem.States;
using Code.Runtime.Services.InputService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class Player : MonoBehaviour
    {
        private const float ScaleFactor = 0.0001f;
        private const string ObstacleTag = "Obstacle";

        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private PlayerAnimator playerAnimator;

        private PlayerStateMachine _playerStateMachine;
        private PlayerSideMovement _playerSideMovement;

        private Rigidbody _rigidbody;
        private IInputService _inputService;
        private BoxCollider _boxCollider;

        private float speedScale;
        private bool isDead;

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
            _playerStateMachine.RegisterState(new RunState(_rigidbody, _inputService, _playerStateMachine, playerAnimator));
            _playerStateMachine.RegisterState(new JumpState(_rigidbody, _inputService, _playerStateMachine, 3f, playerAnimator));
            _playerStateMachine.RegisterState(new SlidingState(_rigidbody, _inputService, _playerStateMachine,
                _boxCollider, 0.5f, playerAnimator));
            _playerStateMachine.RegisterState(new DeadState(playerAnimator));

            _playerStateMachine.Enter<RunState>();
        }

        public void Update()
        {
            if (isDead) return;

            _playerStateMachine.UpdateState();
            _playerSideMovement.UpdatePosition();

            transform.Translate(Vector3.forward * ((playerConfig.StartMoveSpeed + speedScale) * Time.deltaTime));

            speedScale += playerConfig.MoveSpeedScaler * ScaleFactor;
        }

        public void OnTriggerEnter(Collider other)
        {
            GameObject obstacleGameObject = other.gameObject;

            if (obstacleGameObject.CompareTag(ObstacleTag))
            {
                if (obstacleGameObject.TryGetComponent(out Obstacle obstacle))
                {
                    isDead = true;

                    _playerStateMachine.Enter<DeadState>();
                }
            }
        }
    }
}