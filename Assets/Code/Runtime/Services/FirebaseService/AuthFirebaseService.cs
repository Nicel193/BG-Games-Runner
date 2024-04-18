using System;
using System.Threading.Tasks;
using Code.Runtime.Services.LogService;
using Firebase;
using Firebase.Auth;
using UnityEngine;

namespace Code.Runtime.Services.FirebaseService
{
    public class AuthFirebaseService : IDisposable, IAuthFirebaseService
    {
        public const string CompleteRegistration = "Complete Registration";
        
        private FirebaseAuth auth;
        private FirebaseUser user;

        private readonly ILogService _logService;

        public AuthFirebaseService(ILogService logService)
        {
            _logService = logService;
        }

        public void Initialize()
        {
            _logService.Log("Setting up Firebase Auth");
            auth = FirebaseAuth.DefaultInstance;
            auth.StateChanged += AuthStateChanged;

            AuthStateChanged(this, null);
        }

        private void AuthStateChanged(object sender, EventArgs eventArgs)
        {
            if (auth.CurrentUser != user)
            {
                bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
                if (!signedIn && user != null)
                {
                    _logService.Log("Signed out " + user.UserId);
                }

                user = auth.CurrentUser;
                if (signedIn)
                {
                    _logService.Log("Signed in " + user.UserId);
                }
            }
        }

        private async Task Login(string _email, string _password)
        {
            try
            {
                // Call the Firebase auth signin function passing the email and password
                AuthResult authResult = await auth.SignInWithEmailAndPasswordAsync(_email, _password);

                // User is now logged in
                Debug.LogFormat("User signed in successfully: {0} ({1})", authResult.User.DisplayName,
                    authResult.User.Email);
                // warningLoginText.text = "";
                // confirmLoginText.text = "Logged In";
            }
            catch (FirebaseException firebaseEx)
            {
                AuthError errorCode = (AuthError) firebaseEx.ErrorCode;

                string message = "Login Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WrongPassword:
                        message = "Wrong Password";
                        break;
                    case AuthError.InvalidEmail:
                        message = "Invalid Email";
                        break;
                    case AuthError.UserNotFound:
                        message = "Account does not exist";
                        break;
                }

                Debug.Log(message);
                // warningLoginText.text = message;
            }
            catch (Exception ex)
            {
                // If there are errors handle them
                Debug.LogWarning($"Failed to login with {ex}");
                // warningLoginText.text = "Login Failed!";
            }
        }

        public async Task<string> Register(string email, string password, string repeatedPassword, string username)
        {
            if (username == "")
                return "Missing Username";

            if (password != repeatedPassword) 
                return "Password Does Not Match!";

            try
            {
                var authResult = await auth.CreateUserWithEmailAndPasswordAsync(email, password);

                if (authResult != null)
                {
                    UserProfile profile = new UserProfile {DisplayName = username};

                    await authResult.User.UpdateUserProfileAsync(profile);
                }
            }
            catch (FirebaseException firebaseEx)
            {
                AuthError errorCode = (AuthError) firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }

                return message;
            }
            catch (Exception ex)
            {
                return $"Failed to register task with {ex}";
            }

            return CompleteRegistration;
        }

        public void Dispose()
        {
            auth.StateChanged -= AuthStateChanged;
            auth = null;
        }
    }
}