using System;

namespace Core.Interfaces
{
    public interface IGameInput
    {
        public event Action OnScreenTapped;
    }
}