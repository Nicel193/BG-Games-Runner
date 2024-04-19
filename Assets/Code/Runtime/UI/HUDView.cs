using Code.Runtime.Interactors;
using Code.Runtime.Repositories;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI
{
    public class HUDView : MonoBehaviour, IHudView
    {
        [SerializeField] private TextMeshProUGUI maxScoreText;
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private IInteractorContainer _interactorContainer;

        [Inject]
        public void Construct(IInteractorContainer interactorContainer)
        {
            _interactorContainer = interactorContainer;
        }

        private void Awake()
        {
            if (!_interactorContainer.TryGet(out UserInteractor userInteractor)) return;

            userInteractor.OnScoreIncreased += UpdateScoreText;
            
            UpdateScoreText(userInteractor.GetCurrentScore(), userInteractor.GetMaxScore());
        }

        private void OnDestroy()
        {
            if (!_interactorContainer.TryGet(out UserInteractor userInteractor)) return;

            userInteractor.OnScoreIncreased -= UpdateScoreText;
        }
        
        public void Enable() =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);

        private void UpdateScoreText(int score, int maxScore)
        {
            scoreText.text = score.ToString();
            maxScoreText.text = maxScore.ToString();
        }
    }
}