using Code.Runtime.UI.Windows;
using UnityEngine;

namespace Code.Runtime.UI
{
    public interface IWindowFactory
    {
        Transform CreateWindowsRoot();
        T CreateWindow<T>(WindowType windowType) where T : WindowBase;
    }
}