using System.Threading.Tasks;
using Code.Runtime.Repositories;

namespace Code.Runtime.Services.DatabaseService
{
    public interface IDatabaseService
    {
        void SaveUserDataAsync(string userId);
        Task<UserRepository> GetUserDataAsync(string userId, string userName);
    }
}