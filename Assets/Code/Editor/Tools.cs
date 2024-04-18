using Firebase.Auth;
using UnityEditor;

namespace Code.Editor
{
    public class Tools 
    {
        [MenuItem("Tools/ClearAuthData")]
        public static void ClearAuthData()
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }
}