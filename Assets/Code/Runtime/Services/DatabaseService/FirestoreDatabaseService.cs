using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Runtime.Repositories;
using Firebase.Firestore;

namespace Code.Runtime.Services.DatabaseService
{
    public class FirestoreDatabaseService : IDatabaseService
    {
        private const string UsersCollection = "users";

        private FirebaseFirestore _firebaseFirestore;
        private UserRepository _userRepository;

        public FirestoreDatabaseService()
        {
            _firebaseFirestore = FirebaseFirestore.DefaultInstance;
        }

        public async void SaveUserDataAsync(string userId)
        {
            if(_userRepository == null) return;

            DocumentReference docRef = _firebaseFirestore.Collection(UsersCollection).Document(userId);

            await docRef.SetAsync(_userRepository);
        }

        public async Task<UserRepository> GetUserDataAsync(string userId, string userName)
        {
            DocumentReference docRef = _firebaseFirestore.Collection(UsersCollection).Document(userId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                _userRepository = snapshot.ConvertTo<UserRepository>();

                return _userRepository;
            }

            _userRepository = new UserRepository()
            {
                Name = userName,
            };

            return _userRepository;
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