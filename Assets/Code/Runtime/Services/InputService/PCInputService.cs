using System;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Services.InputService
{
    public class PCInputService : IInputService, ITickable
    {
        public event Action OnLeftMove;
        public event Action OnRightMove;
        public event Action OnJump;
        public event Action OnSliding;

        public void Tick() =>
            UpdateInput();

        private void UpdateInput()
        {
            if(Input.GetKeyDown(KeyCode.A)) OnLeftMove?.Invoke();
            if(Input.GetKeyDown(KeyCode.D)) OnRightMove?.Invoke();
            if(Input.GetKeyDown(KeyCode.S)) OnSliding?.Invoke();
            if(Input.GetKeyDown(KeyCode.Space)) OnJump?.Invoke();
        }
    }
}