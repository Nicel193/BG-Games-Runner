using System.Threading.Tasks;
using Code.Runtime.Repositories;
using Code.Runtime.Services.AuthService;
using Firebase.Firestore;

namespace Code.Runtime.Services.DatabaseService
{
    public class FirestoreDatabaseService : IDatabaseService
    {
        private const string UsersCollection = "users";
        
        private FirebaseFirestore _firebaseFirestore;
        private IAuthService _authService;

        public FirestoreDatabaseService(IAuthService authService)
        {
            _authService = authService;
            _firebaseFirestore = FirebaseFirestore.DefaultInstance;
        }

        public async Task SaveUserDataAsync(string userId, UserRepository userData)
        {
            DocumentReference docRef = _firebaseFirestore.Collection(UsersCollection).Document(userId);
            
            await docRef.SetAsync(userData);
        }

        public async Task<UserRepository> GetUserDataAsync(string userId)
        {
            DocumentReference docRef = _firebaseFirestore.Collection(UsersCollection).Document(userId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        
            if (snapshot.Exists)
            {
                UserRepository userData = snapshot.ConvertTo<UserRepository>();
                return userData;
            }

            return new UserRepository()
            {
                Name = _authService.UserName,
            };
        }
    }
}