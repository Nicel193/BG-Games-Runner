using Code.Runtime.Repositories;
using Code.Runtime.Services.WindowsService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button CloseButton;
        
        protected IInteractorContainer InteractorContainer { get; set; }
        protected IWindowService WindowService;

        [Inject]
        public void Construct(IWindowService windowService, IInteractorContainer interactorContainer)
        {
            InteractorContainer = interactorContainer;
            WindowService = windowService;
        }

        private void Awake() =>
            Initialize();

        protected virtual void OnEnable()
        {
            CloseButton.onClick.AddListener(WindowService.Close);
            
            SubscribeUpdates();
        }

        protected virtual void OnDisable()
        {
            CloseButton.onClick.RemoveListener(WindowService.Close);
            
            Cleanup();
        }

        protected abstract void Initialize();
        protected abstract void SubscribeUpdates();
        protected abstract void Cleanup();
    }
}