using System.Collections.Generic;
using Code.Runtime.Interactors;
using Code.Runtime.Repositories;
using Code.Runtime.Services.DatabaseService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public class LeaderboardWindow : WindowBase
    {
        [SerializeField] private LeaderboardItem leaderboardItemPrefab;
        [SerializeField] private Transform leaderboardItemsContainer;
        [SerializeField] private TextMeshProUGUI userMaxScoreText;

        private IDatabaseService _databaseService;
        private UserInteractor _userInteractor;

        [Inject]
        private void Construct(IDatabaseService databaseService, IInteractorContainer interactorContainer)
        {
            _databaseService = databaseService;

            _userInteractor = interactorContainer.Get<UserInteractor>();
        } 
        
        protected override void Initialize()
        {
            InitializeLeaderboardItems();

            userMaxScoreText.text = $"Your max score: {_userInteractor.GetMaxScore().ToString()}";
        }

        protected override void SubscribeUpdates()
        {
        }

        protected override void Cleanup()
        {
            foreach (Transform item in leaderboardItemsContainer)
                Destroy(item.gameObject);
        }

        private async void InitializeLeaderboardItems()
        {
            List<UserRepository> topPlayers = await _databaseService.GetTopPlayersAsync(5);

            for (int i = 0; i < topPlayers.Count; i++)
            {
                LeaderboardItem leaderboardItem = Instantiate(leaderboardItemPrefab, leaderboardItemsContainer);
                
                leaderboardItem.Initialize(i, topPlayers[i].Name, topPlayers[i].MaxScore);
            }
        }
    }
}