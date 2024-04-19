using Code.Runtime.Interactors;
using Code.Runtime.Repositories;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI
{
    public class HUDView : MonoBehaviour
    {
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
        }

        private void OnDestroy()
        {
            if (!_interactorContainer.TryGet(out UserInteractor userInteractor)) return;

            userInteractor.OnScoreIncreased -= UpdateScoreText;
        }

        private void UpdateScoreText(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}