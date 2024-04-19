using Code.Runtime.Services.WindowsService;
using Code.Runtime.UI.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI
{
    public class OpenWindowButton : MonoBehaviour
    {
        [Tooltip("Return to previous page")]
        public bool ReturnPage;
        public Button Button;
        public WindowType WindowType;

        private IWindowService _windowService;

        [Inject]
        public void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Start() => 
            Button.onClick.AddListener(Open);

        private void Open() =>
            _windowService.Open(WindowType, ReturnPage);
    }
}