using System.Threading.Tasks;

namespace Code.Runtime.Services.AuthService
{
    public interface IAuthService
    {
        bool IsUserAuth { get; }
        string UserId { get; }
        string UserName { get; }

        void Initialize();
        Task<string> Register(string email, string password, string repeatedPassword, string username);
        Task<string> Login(string email, string password);
        void SignOut();
    }
}