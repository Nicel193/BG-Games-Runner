using System;

namespace Code.Runtime.UI
{
    public interface IMenuView
    {
        event Action OnStartGame;
        void Enable();
        void Disable();
    }
}