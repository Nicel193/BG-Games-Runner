using System;
using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States;
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
        private ISceneLoader _sceneLoader;

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
        private void Construct(IAuthService authService, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _authService = authService;
        }

        public void Enable() =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);

        private void SignOut()
        {
            _authService.SignOut();
            _sceneLoader.Load(SceneName.Authorization.ToString());
        }

        private void StartGame() =>
            OnStartGame?.Invoke();
    }
}