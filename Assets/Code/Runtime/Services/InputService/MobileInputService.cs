using System;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Services.InputService
{
    public class MobileInputService : IInputService, ITickable
    {
        public float swipeThreshold = 100f;
        private Vector2 fingerDownPosition;
        private Vector2 fingerUpPosition;

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
                    fingerDownPosition = touch.position;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    fingerUpPosition = touch.position;

                    if (Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x) > swipeThreshold)
                    {
                        if (fingerDownPosition.x - fingerUpPosition.x > 0)
                        {
                            OnLeftMove?.Invoke();
                        }
                        else
                        {
                            OnRightMove?.Invoke();
                        }
                    }
                    else if (Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y) > swipeThreshold)
                    {
                        if (fingerDownPosition.y - fingerUpPosition.y > 0)
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