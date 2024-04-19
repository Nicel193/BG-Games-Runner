using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Windows
{
    public class LeaderboardItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void Initialize(int index, string playerName, int playerScore)
        {
            nameText.text = $"{index}. {playerName}";
            scoreText.text = playerScore.ToString();
        }
    }
}