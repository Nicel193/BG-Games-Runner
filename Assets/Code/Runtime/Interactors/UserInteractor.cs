using System;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class UserInteractor : Interactor<UserRepository>
    {
        private const int MaxDeathCount = 1;
        
        public event Action<int, int> OnScoreIncreased;
        
        public int GetMaxScore() =>
            _repository.MaxScore;

        public int GetCurrentScore() =>
            _repository.CurrentScore;

        public void IncreaseDeathCount() =>
            _repository.DeathCount++;

        public bool CanRespawn() =>
            _repository.DeathCount <= MaxDeathCount;

        public void AddCurrentScore(int CurrentScore)
        {
            if (CurrentScore < 0) return;

            _repository.CurrentScore += CurrentScore;

            if (_repository.CurrentScore > _repository.MaxScore)
                _repository.MaxScore = _repository.CurrentScore;

            OnScoreIncreased?.Invoke(_repository.CurrentScore, _repository.MaxScore);
        }

        public void Clear()
        {
            _repository.CurrentScore = 0;
            _repository.DeathCount = 0;
        }
    }
}