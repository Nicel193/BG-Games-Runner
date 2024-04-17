using Code.Runtime.Logic.Map;
using Code.Runtime.Logic.PlayerSystem.States;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class Player : MonoBehaviour, IReadonlyPlayer
    {
        private const float ScaleFactor = 0.0001f;
        private const string ObstacleTag = "Obstacle";
        
        public Rigidbody Rigidbody { get; private set; }
        public BoxCollider BoxCollider { get; private set; }
        
        private PlayerStateMachine _playerStateMachine;
        private PlayerStatesFactory _playerStatesFactory;
        
        private PlayerSideMovement _playerSideMovement;

        private float speedScale;
        private bool isDead;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            BoxCollider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            // _playerSideMovement = new PlayerSideMovement(Rigidbody, _inputService,
            //     playerConfig.SideMoveOffset, playerConfig.ChangeSideSpeed);
            // _playerStateMachine = new PlayerStateMachine();

            RegisterStates();
            
            _playerStateMachine.Enter<DeadState>();
        }

        [Inject]
        private void Construct(PlayerStateMachine playerStateMachine, PlayerStatesFactory playerStatesFactory)
        {
            _playerStatesFactory = playerStatesFactory;
            _playerStateMachine = playerStateMachine;
        }

        private void RegisterStates()
        {
            _playerStateMachine.RegisterState(_playerStatesFactory.Create<DeadState>());
            
            // _playerStateMachine.RegisterState(new RunState(_rigidbody, _inputService, _playerStateMachine, playerAnimator));
            // _playerStateMachine.RegisterState(new JumpState(_rigidbody, _inputService, _playerStateMachine, 4f, playerAnimator));
            // _playerStateMachine.RegisterState(new SlidingState(_rigidbody, _inputService, _playerStateMachine,
            //     _boxCollider, 0.5f, playerAnimator));
            // _playerStateMachine.RegisterState(new DeadState(playerAnimator));

            // _playerStateMachine.Enter<RunState>();
        }

        public void Update()
        {
            if (isDead) return;

            // _playerStateMachine.UpdateState();
            // _playerSideMovement.UpdatePosition();

            // transform.Translate(Vector3.forward * ((playerConfig.StartMoveSpeed + speedScale) * Time.deltaTime));

            // speedScale += playerConfig.MoveSpeedScaler * ScaleFactor;
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