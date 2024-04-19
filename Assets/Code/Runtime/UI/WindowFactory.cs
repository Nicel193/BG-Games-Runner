using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Configs;
using Code.Runtime.UI.Windows;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IInstantiator _instantiator;
        
        private Transform _uiRoot;
        private Dictionary<WindowType, GameObject> _windowAssets = new Dictionary<WindowType, GameObject>();
        private WindowAssetsConfig _windowAssetsConfig;

        public WindowFactory(IInstantiator instantiator, WindowAssetsConfig windowAssetsConfig)
        {
            _windowAssetsConfig = windowAssetsConfig;
            _instantiator = instantiator;
            
            _windowAssets = _windowAssetsConfig.WindowAssets
                .ToDictionary(k => k.WindowType, v => v.WindowPrefab);
        }

        public Transform CreateWindowsRoot()
        {
            _uiRoot = Instantiate<Transform>(_windowAssetsConfig.WindowRootPrefab);

            return _uiRoot;
        }

        public T CreateWindow<T>(WindowType windowType) where T : WindowBase
        {
            T instantiateWindow =
                Instantiate<T>(_windowAssets[windowType], _uiRoot);

            return instantiateWindow;
        }

        private T Instantiate<T>(GameObject prefab, Transform parent = null) where T : Object
        {
            GameObject instantiatePrefab = _instantiator.InstantiatePrefab(prefab, parent);

            return instantiatePrefab.GetComponent<T>();
        }
    }
}