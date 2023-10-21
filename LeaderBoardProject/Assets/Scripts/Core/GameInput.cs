using System;
using Core.Interfaces;
using UnityEngine;

namespace Core
{
    public class GameInput : IGameInput
    {
        public event Action OnScreenTapped;
        
        public void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                OnScreenTapped?.Invoke();
            }
        }
    }
}