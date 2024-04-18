using Firebase.Firestore;

namespace Code.Runtime.Repositories
{
    [FirestoreData]
    public struct UserRepository
    {
        [FirestoreProperty]
        public string Name { get; set; }
       
        [FirestoreProperty]
        public int Score { get; set; }
    }
}