using System;
using UnityEngine;

namespace Core
{
    public class GameInput
    {
        public event Action OnScreenTapped;
        
        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                OnScreenTapped?.Invoke();
            }
        }
    }
}