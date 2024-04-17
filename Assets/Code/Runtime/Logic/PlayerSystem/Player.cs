using Code.Runtime.Logic.Map;
using Code.Runtime.Logic.PlayerSystem.States;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.PlayerSystem
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class Player : MonoBehaviour, IReadonlyPlayer
    {
        private const string ObstacleTag = "Obstacle";
        
        public Rigidbody Rigidbody { get; private set; }
        public BoxCollider BoxCollider { get; private set; }
        
        private PlayerStateMachine _playerStateMachine;
        private PlayerStatesFactory _playerStatesFactory;
        
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            BoxCollider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            RegisterStates();
            
            _playerStateMachine.Enter<RunStateTmp>();
        }

        [Inject]
        private void Construct(PlayerStateMachine playerStateMachine, PlayerStatesFactory playerStatesFactory)
        {
            _playerStatesFactory = playerStatesFactory;
            _playerStateMachine = playerStateMachine;
        }

        private void Update() =>
            _playerStateMachine.UpdateState();

        private void RegisterStates()
        {
            _playerStateMachine.RegisterState(_playerStatesFactory.Create<DeadState>());
            _playerStateMachine.RegisterState(_playerStatesFactory.Create<RunStateTmp>());
            _playerStateMachine.RegisterState(_playerStatesFactory.Create<JumpState>());
            _playerStateMachine.RegisterState(_playerStatesFactory.Create<SlidingState>());
        }

        public void OnTriggerEnter(Collider other)
        {
            GameObject obstacleGameObject = other.gameObject;

            if (!obstacleGameObject.CompareTag(ObstacleTag)) return;
            
            if (obstacleGameObject.TryGetComponent(out Obstacle obstacle))
                _playerStateMachine.Enter<DeadState>();
        }
    }
}