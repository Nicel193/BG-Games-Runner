using System;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class UserInteractor : Interactor<UserRepository>
    {
        public event Action<int, int> OnScoreIncreased;
        
        public int GetMaxCurrentScore() =>
            _repository.CurrentScore;

        public int GetCurrentScore() =>
            _repository.CurrentScore;

        public void ResetCurrentScore() =>
            _repository.CurrentScore = 0;

        public void AddCurrentScore(int CurrentScore)
        {
            if (CurrentScore < 0) return;

            _repository.CurrentScore += CurrentScore;

            if (_repository.CurrentScore > _repository.MaxScore)
                _repository.MaxScore = _repository.CurrentScore;

            OnScoreIncreased?.Invoke(_repository.CurrentScore, _repository.MaxScore);
        }
    }
}