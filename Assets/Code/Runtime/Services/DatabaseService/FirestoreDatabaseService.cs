using System.Collections.Generic;
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

        public FirestoreDatabaseService()
        {
            _firebaseFirestore = FirebaseFirestore.DefaultInstance;
        }

        public async Task SaveUserDataAsync(string userId, UserRepository userData)
        {
            DocumentReference docRef = _firebaseFirestore.Collection(UsersCollection).Document(userId);
            
            await docRef.SetAsync(userData);
        }

        public async Task<UserRepository> GetUserDataAsync(string userId, string userName)
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
                Name = userName,
            };
        }
        
        public async Task<List<UserRepository>> GetTopPlayersAsync(int limit)
        {
            Query query = _firebaseFirestore.Collection(UsersCollection)
                .OrderByDescending("score")
                .Limit(limit);

            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            List<UserRepository> topPlayers = new List<UserRepository>();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                UserRepository player = documentSnapshot.ConvertTo<UserRepository>();
                topPlayers.Add(player);
            }

            return topPlayers;
        }
    }
}