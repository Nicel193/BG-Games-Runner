using Firebase.Firestore;

namespace Code.Runtime.Repositories
{
    [FirestoreData]
    public class UserRepository : IRepository
    {
        [FirestoreProperty]
        public string Name { get; set; }
       
        [FirestoreProperty]
        public int MaxScore { get; set; }
        
        public int CurrentScore { get; set; }
    }
}