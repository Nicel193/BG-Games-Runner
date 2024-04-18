using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Services.AuthService;
using Firebase.Firestore;

namespace Code.Runtime.Infrastructure.States.Core
{
   public class BootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAuthService _authService;

        public BootstrapState(ISceneLoader sceneLoader, GameStateMachine gameStateMachine, IAuthService authService)
        {
            _authService = authService;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _authService.Initialize();

            FirebaseFirestore firebaseFirestore = FirebaseFirestore.DefaultInstance;

            // UserData userData = new UserData()
            // {
            //     Name = _authService.UserName,
            //     Score = 999,
            // };
            //
            // firebaseFirestore.Document($"Test/{_authService.UserId}").SetAsync(userData);

            _sceneLoader.Load(SceneName.Bootstrap.ToString(), ToLoadProgressState);
        }

        private void ToLoadProgressState() =>
            _gameStateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
        }
    }

   [FirestoreData]
   public struct UserData
   {
       [FirestoreProperty]
       public string Name { get; set; }
       
       [FirestoreProperty]
       public int Score { get; set; }
   }
}