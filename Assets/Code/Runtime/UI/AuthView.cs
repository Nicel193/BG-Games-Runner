using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Infrastructure.States.Core;
using Code.Runtime.Services.AuthService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI
{
    public class AuthWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI errorMessage;
        
        [Header("Registration")]
        [SerializeField] private TMP_InputField userNameInputField;
        [SerializeField] private TMP_InputField emailNameInputField;
        [SerializeField] private TMP_InputField passwordNameInputField;
        [SerializeField] private TMP_InputField confirmPasswordNameInputField;
        [SerializeField] private Button registerButton;

        [Header("Login")]
        [SerializeField] private TMP_InputField loginEmailNameInputField;
        [SerializeField] private TMP_InputField loginPasswordNameInputField;
        [SerializeField] private Button loginButton;

        [Header("Forms")] 
        [SerializeField] private GameObject registrationForm;
        [SerializeField] private GameObject loginForm;
        [SerializeField] private Button toRegistrationFormButton;
        [SerializeField] private Button toLoginFormButton;

        private ISceneLoader _sceneLoader;
        private IAuthService _authService;
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            registerButton.onClick.AddListener(Register);
            loginButton.onClick.AddListener(Login);
            toRegistrationFormButton.onClick.AddListener(ToRegistrationForm);
            toLoginFormButton.onClick.AddListener(ToLoginForm);
        }

        private void OnDestroy()
        {
            registerButton.onClick.RemoveListener(Register);
            toRegistrationFormButton.onClick.RemoveListener(ToRegistrationForm);
            toLoginFormButton.onClick.RemoveListener(ToLoginForm);
        }

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IAuthService authService)
        {
            _gameStateMachine = gameStateMachine;
            _authService = authService;
        }

        private async void Register()
        {
            string registerMessage = await _authService.Register(
                emailNameInputField.text,
                passwordNameInputField.text,
                confirmPasswordNameInputField.text,
                userNameInputField.text);
            
            if (registerMessage == AuthFirebaseService.CompletedRegistration)
            {
                _gameStateMachine.Enter<LoadProgressState>();

                return;
            }

            errorMessage.text = registerMessage;
        }

        private async void Login()
        {
            string loginMessage = await _authService.Login(
                loginEmailNameInputField.text,
                loginPasswordNameInputField.text);

            if (loginMessage == AuthFirebaseService.CompletedLogin)
            {
                _gameStateMachine.Enter<LoadProgressState>();
                
                return;
            }

            errorMessage.text = loginMessage;
        }

        private void ToLoginForm() =>
            ChangeForm(true);

        private void ToRegistrationForm() =>
            ChangeForm(false);

        private void ChangeForm(bool isActiveLoginForm)
        {
            registrationForm.SetActive(!isActiveLoginForm);
            loginForm.SetActive(isActiveLoginForm);
        }
    }
}