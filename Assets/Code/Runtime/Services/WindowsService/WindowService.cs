﻿using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Services.LogService;
using Code.Runtime.UI;
using Code.Runtime.UI.Windows;

namespace Code.Runtime.Services.WindowsService
{
   public class WindowService : IWindowService
    {
        private readonly IWindowFactory _windowFactory;
        private readonly ILogService _logService;
        // private readonly IAudioService _audioService;

        private WindowBase _currentWindow;
        private WindowType _currentWindowType;
        private IWindowsAnimation _windowsAnimation;

        private List<WindowType> _previousPages = new List<WindowType>();
        private Dictionary<WindowType, WindowBase> _createdWindows = new Dictionary<WindowType, WindowBase>();

        public WindowService(IWindowFactory windowFactory, ILogService logService)
        {
            _windowFactory = windowFactory;
            _logService = logService;
        }
        
        public void Initialize()
        {
            var windowsRoot = _windowFactory.CreateWindowsRoot();

            _windowsAnimation = new WindowsAnimation(windowsRoot);
        }

        public void Open(WindowType windowType, bool returnPage = false)
        {
            SetWindow(windowType, returnPage);

            if (!TryShowCreatedWindow(windowType))
            {
                switch (windowType)
                {
                    case WindowType.Death:
                        _currentWindow = _windowFactory.CreateWindow<DeathWindow>(windowType);
                        break;
                }

                _createdWindows.Add(windowType, _currentWindow);
            }

            // _windowsAnimation.OpenAnimation(_currentWindow.transform);
        }

        private bool TryShowCreatedWindow(WindowType windowType)
        {
            bool isCreatedWindow = (_createdWindows.TryGetValue(windowType, out WindowBase windowObject));

            if (isCreatedWindow)
            {
                windowObject.gameObject.SetActive(true);
                
                _currentWindow = windowObject;
            }

            return isCreatedWindow;
        }

        public void Close()
        {
            if (_currentWindow == null)
            {
                _logService.LogError("Window is closed");

                return;
            }

            DestroyWindow();

            if (_previousPages.Count > 0)
            {
                WindowType windowType = _previousPages.Last();
                _previousPages.Remove(windowType);
                Open(windowType);
            }
        }

        private void DestroyWindow()
        {
            _currentWindow.gameObject.SetActive(false);
            _currentWindow = null;
            
            // _windowsAnimation.CloseAnimation(_currentWindow.transform, () =>
            // {
            //     _currentWindow.gameObject.SetActive(false);
            //     _currentWindow = null;
            // });
        }

        private void SetWindow(WindowType windowType, bool returnPage)
        {
            if (_currentWindow != null)
            {
                if (returnPage) _previousPages.Add(_currentWindowType);

                DestroyWindow();
            }

            _currentWindowType = windowType;
        }
    }
}