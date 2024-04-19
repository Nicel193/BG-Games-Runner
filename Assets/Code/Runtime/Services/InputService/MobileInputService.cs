using System;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Services.InputService
{
    public class MobileInputService : IInputService, ITickable
    {
        private const float SwipeThreshold = 100f;
        
        private Vector2 _fingerDownPosition;
        private Vector2 _fingerUpPosition;

        public event Action OnLeftMove;
        public event Action OnRightMove;
        public event Action OnJump;
        public event Action OnSliding;

        public void Tick() =>
            CheckSwipe();

        private void CheckSwipe()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _fingerDownPosition = touch.position;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    _fingerUpPosition = touch.position;

                    if (Mathf.Abs(_fingerDownPosition.x - _fingerUpPosition.x) > SwipeThreshold)
                    {
                        if (_fingerDownPosition.x - _fingerUpPosition.x > 0)
                        {
                            OnLeftMove?.Invoke();
                        }
                        else
                        {
                            OnRightMove?.Invoke();
                        }
                    }
                    else if (Mathf.Abs(_fingerDownPosition.y - _fingerUpPosition.y) > SwipeThreshold)
                    {
                        if (_fingerDownPosition.y - _fingerUpPosition.y > 0)
                        {
                            OnSliding?.Invoke();
                        }
                        else
                        {
                            OnJump?.Invoke();
                        }
                    }
                }
            }
        }
    }
}