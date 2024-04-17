using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI
{
    public class MenuView : MonoBehaviour, IMenuView
    {
        public event Action OnStartGame;
        
        [SerializeField] private Button startGameButton;

        private void OnEnable() =>
            startGameButton.onClick.AddListener(StartGame);

        private void OnDisable() =>
            startGameButton.onClick.RemoveListener(StartGame);

        
        public void Enable() =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);
        
        private void StartGame() =>
            OnStartGame?.Invoke();
    }
}