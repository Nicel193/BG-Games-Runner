using System;
using System.Threading.Tasks;
using Code.Runtime.Services.LogService;
using Firebase;
using Firebase.Auth;

namespace Code.Runtime.Services.AuthService
{
    public class AuthFirebaseService : IDisposable, IAuthService
    {
        public const string CompletedLogin = "Completed Login";
        public const string CompletedRegistration = "Completed Registration";
        
        public bool IsUserAuth { get; private set; }
        public string UserId => _user.UserId;
        public string UserName => _user.DisplayName;

        private readonly ILogService _logService;
        private FirebaseAuth _auth;
        private FirebaseUser _user;

        public AuthFirebaseService(ILogService logService) =>
            _logService = logService;

        public void Initialize()
        {
            _logService.Log("Setting up Firebase Auth");

            _auth = FirebaseAuth.DefaultInstance;
            _auth.StateChanged += AuthStateChanged;

            AuthStateChanged(this, null);
        }

        private void AuthStateChanged(object sender, EventArgs eventArgs)
        {
            if (_auth.CurrentUser != _user)
            {
                bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;
                if (!signedIn && _user != null)
                {
                    _logService.Log("Signed out " + _user.UserId);
                }

                _user = _auth.CurrentUser;
                if (signedIn)
                {
                    _logService.Log("Signed in " + _user.UserId);
                }

                IsUserAuth = signedIn;
            }
        }

        public async Task<string> Login(string email, string password)
        {
            try
            {
                AuthResult authResult = await _auth.SignInWithEmailAndPasswordAsync(email, password);

                _logService.Log(
                    $"User signed in successfully: {authResult.User.DisplayName} ({authResult.User.Email})");
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

                return message;
            }
            catch (Exception ex)
            {
                _logService.LogWarning($"Failed to login with {ex}");
            }

            return CompletedLogin;
        }

        public async Task<string> Register(string email, string password, string repeatedPassword, string username)
        {
            if (username == "")
                return "Missing Username";

            if (password != repeatedPassword)
                return "Password Does Not Match!";

            try
            {
                var authResult = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);

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
                _logService.LogWarning($"Failed to register task with {ex}");
            }

            return CompletedRegistration;
        }

        public void SignOut()
        {
            _auth.SignOut();
            _logService.Log("Calling SignOut");
        }

        public void Dispose()
        {
            _auth.StateChanged -= AuthStateChanged;
            _auth = null;
        }
    }
}