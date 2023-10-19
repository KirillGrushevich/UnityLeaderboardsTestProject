using System;
using UnityEngine;

namespace Core
{
    public class EventFunctionsGO : MonoBehaviour
    {
        public event Action OnUpdateGO;

        private void Update()
        {
            OnUpdateGO?.Invoke();
        }
    }
}