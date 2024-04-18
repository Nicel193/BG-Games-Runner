using System;
using System.Threading.Tasks;

namespace Code.Runtime.Services.FirebaseService
{
    public interface IAuthFirebaseService
    {
        event Action OnUserLogout;
        bool IsUserAuth { get; }

        void Initialize();
        Task<string> Register(string email, string password, string repeatedPassword, string username);
        Task<string> Login(string email, string password);
    }
}