using System.Threading.Tasks;

namespace Code.Runtime.Services.FirebaseService
{
    public interface IAuthFirebaseService
    {
        void Initialize();
        Task<string> Register(string email, string password, string repeatedPassword, string username);
    }
}