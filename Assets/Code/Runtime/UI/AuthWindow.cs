using System;
using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.FirebaseService;
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
        private IAuthFirebaseService _authFirebaseService;

        private void Awake()
        {
            registerButton.onClick.AddListener(Register);
            toRegistrationFormButton.onClick.AddListener(ToRegistrationForm);
            toLoginFormButton.onClick.AddListener(ToLoginForm);
        }

        private void OnDestroy()
        {
            registerButton.onClick.RemoveListener(Register);
            toRegistrationFormButton.onClick.RemoveListener(ToRegistrationForm);
        }

        [Inject]
        private void Construct(ISceneLoader sceneLoader, IAuthFirebaseService authFirebaseService)
        {
            _authFirebaseService = authFirebaseService;
            _sceneLoader = sceneLoader;
        }

        private async void Register()
        {
            string registerMessage = await _authFirebaseService.Register(
                emailNameInputField.text,
                passwordNameInputField.text,
                confirmPasswordNameInputField.text,
                userNameInputField.text);

            if (registerMessage == AuthFirebaseService.CompleteRegistration)
            {
                _sceneLoader.Load(SceneName.Gameplay.ToString());

                return;
            }

            errorMessage.text = registerMessage;
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