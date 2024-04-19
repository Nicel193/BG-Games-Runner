using System;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Infrastructure.States.Core;
using Code.Runtime.Services.AuthService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI
{
    public class MenuView : MonoBehaviour, IMenuView
    {
        public event Action OnStartGame;

        [SerializeField] private Button startGameButton;
        [SerializeField] private Button signOutGameButton;

        private IAuthService _authService;
        private GameStateMachine _gameStateMachine;

        private void OnEnable()
        {
            startGameButton.onClick.AddListener(StartGame);
            signOutGameButton.onClick.AddListener(SignOut);
        }

        private void OnDisable()
        {
            startGameButton.onClick.RemoveListener(StartGame);
            signOutGameButton.onClick.RemoveListener(SignOut);
        }

        [Inject]
        private void Construct(IAuthService authService, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _authService = authService;
        }

        public void Enable() =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);

        private void SignOut()
        {
            _authService.SignOut();
            _gameStateMachine.Enter<LoadSceneState, string>(SceneName.Authorization.ToString());
        }

        private void StartGame() =>
            OnStartGame?.Invoke();
    }
}