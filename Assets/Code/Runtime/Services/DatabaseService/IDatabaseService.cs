using System.Threading.Tasks;
using Code.Runtime.Repositories;

namespace Code.Runtime.Services.DatabaseService
{
    public interface IDatabaseService
    {
        Task SaveUserDataAsync(string userId, UserRepository userData);
        Task<UserRepository> GetUserDataAsync(string userId);
    }
}