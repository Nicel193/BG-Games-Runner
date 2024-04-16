using System;

namespace Code.Runtime.Services.InputService
{
    public interface IInputService
    {
        event Action OnLeftMove;
        event Action OnRightMove;
        event Action OnJump;
    }
}